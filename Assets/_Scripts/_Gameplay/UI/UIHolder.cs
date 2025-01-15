using Assets.UI;
using Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gameplay.UI
{
    public class UIHolder : MonoBehaviour
    {
        private const string EventPopupName = "Event popup";
        private const string ItemPopupName = "Item popup";
        private const string TranslateRarePath = "Translates/Rare/Rare";

        [Header("Configs")]
        [SerializeField] private TextTranslate _eventTranslate;
        [SerializeField] private TextTranslate _itemTranslate;
        [SerializeField] private Dictionary<Rare, TextTranslate> _itemsRareTranslate = new Dictionary<Rare, TextTranslate>();

        [Header("Settings")]
        [SerializeField] private RectTransform _canvasBase;
        [SerializeField] private PopupView _popupView;
        [SerializeField] private ScrimmerPopupView _scrimmerView;

        [Header("Text")]
        [SerializeField] private Text _coinsCount;

        private PopupView _eventPopupView, _itemPopupView;
        private ReactiveUI<int> _coinCountUi;
        private EventSystemHolder _eventSystemHolder;

        public void Init(EventSystemHolder eventSystemHolder)
        {
            GetRareTextTranslates();

            InitPopups();
            InitReactiveUI();

            _eventSystemHolder = eventSystemHolder;
            
            _eventSystemHolder.ChangedEvent += OnEventChange;
            _eventPopupView.PopupClosed += OnPopupClosed;
            SubscriptionKeeper.GettedNewEvent += OnGettedNewItem;

            SubscriptionKeeper.MimikClosed += OnMimikClose;
            SubscriptionKeeper.MimikActivated += OnMimikActivate;
        }

        public void Dispose()
        {
            _eventSystemHolder.ChangedEvent -= OnEventChange;
            _eventPopupView.PopupClosed -= OnPopupClosed;
            SubscriptionKeeper.GettedNewEvent -= OnGettedNewItem;

            SubscriptionKeeper.MimikClosed -= OnMimikClose;
            SubscriptionKeeper.MimikActivated -= OnMimikActivate;

            _coinCountUi.Dispose();
        }

        private void OnMimikActivate()
        {
            _scrimmerView.gameObject.SetActive(true);
            _scrimmerView.Activate();
        }

        private void OnMimikClose()
        {
            _scrimmerView.Deactivate(() => _scrimmerView.gameObject.SetActive(false));
        }

        private void OnPopupClosed()
        {
            _eventSystemHolder.OnPopupClose();
        }

        private void OnEventChange(EventData data)
        {
            _eventPopupView.Open(data.GetIcon(), data.GetDescription(YG.YandexGame.lang));
        }

        private void OnGettedNewItem(Items item)
        {
            _itemPopupView.Open(item.Icon, _itemsRareTranslate[item.GetRare].GetText(YG.YandexGame.lang));
        }

        private void GetRareTextTranslates()
        {
            foreach (Rare i in Enum.GetValues(typeof(Rare)))
            {
                var item = Resources.Load<TextTranslate>(TranslateRarePath + i.ToString());

                if (item == null)
                    throw new NullReferenceException($"Not found translate in path {TranslateRarePath + i.ToString()}" +
                        $" rare {i.ToString()}");

                _itemsRareTranslate[i] = item;
            }
        }
        
        private PopupView GetPopupView(PopupView view, RectTransform transform, string header, string name)
        {
            var popup = Instantiate(view, transform);

            popup.gameObject.SetActive(false);
            popup.gameObject.name = name;
            popup.Init(header);

            return popup;
        }

        private void InitPopups()
        {
            string language = YG.YandexGame.lang;

            _itemPopupView = GetPopupView(_popupView, _canvasBase, _itemTranslate.GetText(language), ItemPopupName);
            _eventPopupView = GetPopupView(_popupView, _canvasBase, _eventTranslate.GetText(language), EventPopupName);
            
            _scrimmerView.Init();
        }

        private void InitReactiveUI()
        {
            _coinCountUi = new ReactiveUI<int>(PlayerSaves.CoinsInPocket, _coinsCount);
            _coinCountUi.Init();
        }
    }
}
