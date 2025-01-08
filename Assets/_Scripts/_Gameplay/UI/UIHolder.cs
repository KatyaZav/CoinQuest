using Assets.UI;
using UnityEngine;

namespace Assets.Gameplay.UI
{
    public class UIHolder : MonoBehaviour
    {
        private const string EventPopupName = "Event popup";
        private const string ItemPopupName = "Item popup";

        [Header("Configs")]
        [SerializeField] private TextTranslate _eventTranslate;
        [SerializeField] private TextTranslate _itemTranslate;

        [Header("Settings")]
        [SerializeField] private RectTransform _canvasBase;
        [SerializeField] private PopupView _popupView;

        private PopupView _eventPopupView, _itemPopupView;

        public void Init()
        {
            string language = YG.YandexGame.lang;

            _itemPopupView = GetPopupView(_popupView, _canvasBase, _itemTranslate.GetText(language), ItemPopupName);
            _eventPopupView = GetPopupView(_popupView, _canvasBase, _eventTranslate.GetText(language), EventPopupName);
        }

        private PopupView GetPopupView(PopupView view, RectTransform transform, string header, string name)
        {
            var popup = Instantiate(view, transform);

            popup.gameObject.SetActive(false);
            popup.gameObject.name = name;
            popup.Init(header);

            return popup;
        }
    }
}
