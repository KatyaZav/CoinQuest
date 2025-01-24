using Assets.Gameplay.Ipnut;
using Assets.Gameplay.UI;
using Assets.UI;
using Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gameplay
{
    public class GameCycle : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;
        [SerializeField] private ItemView _itemView;
        [SerializeField] private AnswerHolderBehavior _answerHolderBehavior;

        [Header("Slider")]
        [SerializeField] private Image _sliderImage;
        [SerializeField] private int _eventMaxCount;

        [Header("Settings")]
        [SerializeField] private AnimationCurve _failProbability;
        [SerializeField] private Color _dangerousColor;
        [SerializeField] private Color _safeColor;

        [Header("Particles")]
        [SerializeField] private WinParticleBehavior _winParticle;

        private EventSystemHolder _eventSystemHolder;
        ItemsLoader _itemsLoader;

        private CustomSlider _currentSlider;
        private PlayerInput _playerInput;
        private ItemGenerator _itemGenerator;


        private int _currentEventCount;

        public void Init(EventSystemHolder eventSystemHolder, ItemsLoader items)
        {
            _eventSystemHolder = eventSystemHolder;
            _itemsLoader = items;
            _currentEventCount = _eventMaxCount;

            _answerHolderBehavior.Init();
            _itemView.Init();

            CreatePlayerInput();
            CreateCustomSlider();
            CreateItemGenerator(_itemsLoader);

            StartRound();
        }

        public ItemGenerator CurrentItemGenerator => _itemGenerator;

        public void OnDispose()
        {
            DisposePlayerInput();
            DisposeCustomSlider();
        }

        #region CustomSlider
        private void CreateCustomSlider()
        {
            _currentSlider = new CustomSlider(_sliderImage, _eventMaxCount);
            _currentSlider.OnSliderEndEvent += OnSliderEnd;
        }

        private void DisposeCustomSlider()
        {
            _currentSlider.OnSliderEndEvent -= OnSliderEnd;
        }
        #endregion

        #region PlayerInput
        private void DisposePlayerInput()
        {
            _playerInput.ItemCollectedEvent -= OnItemCollectButtonClick;
            _playerInput.ItemDropedEvent -= OnItemDroppedButtonClick;

            _playerInput.OnDispose();
        }

        private void CreatePlayerInput()
        {
            _playerInput = new PlayerInput(_yesButton, _noButton);
            _playerInput.OnInit();

            _playerInput.ItemCollectedEvent += OnItemCollectButtonClick;
            _playerInput.ItemDropedEvent += OnItemDroppedButtonClick;
        }
        #endregion

        #region ItemGenerator
        private void CreateItemGenerator(ItemsLoader loader)
        {
            _itemGenerator = new ItemGenerator(_itemView, _failProbability, _dangerousColor,
                _safeColor, loader, PlayerSaves.CurrentRoom);
        }
        #endregion

        private void OnSliderEnd()
        {
            Debug.Log("Slider ended!");
            _eventSystemHolder.OnNewEvent();
        }

        private void OnItemCollectButtonClick()
        {
            _answerHolderBehavior.Disactivate();
            TryCollectItem();
        }

        private void OnItemDroppedButtonClick()
        {
            _answerHolderBehavior.Disactivate();
            _itemView.ActivateDestroyAnimation(StartRound);
        }

        private void TryCollectItem()
        {
            if (_itemGenerator.IsMimik)
            {
                GetMimik();
            }
            else
            {
                CollectItem();
            }
        }

        private void StartRound()
        {
            _answerHolderBehavior.Activate();

            _itemGenerator.GenerateItem();

            _itemView.SetImage(_itemGenerator.GetImage());
            _itemView.SetTextColor(_itemGenerator.GetColor());
            _itemView.SetProbabilityText(_itemGenerator.GetText());

            _itemView.ActivateStartAnimation();

            PlayerSaves.MakeSeen(_itemGenerator.Item);
        }

        private void CollectItem()
        {
            var win = Instantiate(_winParticle, transform.position, transform.rotation);
            win.Init(_itemGenerator.GetImage());

            _currentSlider.AddValue();

            PlayerSaves.MakeGetted(_itemGenerator.Item);
            PlayerSaves.AddCoins(_itemGenerator.ItemValue);

            _itemView.ActivateCollectAnimation(StartRound);
        }

        private void GetMimik()
        {
            _itemView.ActivateCollectAnimation();

            PlayerSaves.LooseCoins();
            SubscriptionKeeper.MimikActivate(StartRound);
        }
    }
}
