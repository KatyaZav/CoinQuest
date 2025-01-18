using Assets.Menu.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using YG.Insides;

namespace Assets.Menu.Shop
{
    public class DataHolder : MonoBehaviour
    {
        private readonly string PartPath = "Shop/Part";
        private readonly string ColorPath = "Shop/Color";

        [Header("Place items")]
        [SerializeField] private RectTransform _buttonSliderPlace;
        [SerializeField] private RectTransform _buttonShopPlace;

        [Header("Prefabs")]
        [SerializeField] private ShopVariantButton _shopVariantButtonPrefab;
        [SerializeField] private ButtonView _buttonPrefab;

        private List<CatPartConfig> _partsConfigs;
        private List<CatColorConfig> _colorConfigs;

        private List<ButtonView> _buttonsView =  new List<ButtonView>();
        private List<ShopVariantButton> _shopButons = new List<ShopVariantButton>();

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _partsConfigs = Resources.LoadAll<CatPartConfig>(PartPath).ToList();
            _colorConfigs = Resources.LoadAll<CatColorConfig>(ColorPath).ToList();

            GenerateButtonSlider();
            GenerateList(_partsConfigs[0].PartType, false);
        }

        private void OnDestroy()
        {
            MakeUnSubscriber();
        }

        private void GenerateButtonSlider()
        {
            foreach (CatPart e in Enum.GetValues(typeof(CatPart)))
            {
                GenerateButton(e);
            }

            MakeSubscriber();
        }

        private void MakeSubscriber()
        {
            foreach (ButtonView e in _buttonsView)
            {
                e.Clicked += GenerateList;
            }
        }

        private void MakeUnSubscriber()
        {
            foreach (ButtonView e in _buttonsView)
            {
                e.Clicked -= GenerateList;
            }
        }

        private void GenerateList(CatPart type, bool isColor)
        {
            UnSubscribeShopButtons();

            if (isColor)
            {
                foreach (CatColorConfig config in _colorConfigs)
                    GetColorVariant(config);
            }
            else
            {
                List<CatPartConfig> part = _partsConfigs.FindAll(item => item.PartType == type);

                foreach (CatPartConfig config in part)
                {
                    GetShopVariant(config);
                }
            }

            SubscribeShopButtons();
        }

        private void SubscribeShopButtons()
        {
            foreach ( var e in _shopButons)
            {
                e.Clicked += OnChooseShopItem;
            }
        }

        private void UnSubscribeShopButtons()
        {
            foreach (var e in _shopButons)
            {
                if (e!= null)
                {
                    e.Clicked -= OnChooseShopItem;
                    Destroy(e.gameObject);
                }
            }
        }

        private void OnChooseShopItem()
        {
            throw new NotImplementedException();
        }

        private void GenerateButton(CatPart type)
        {
            CatPartConfig currentConfing = _partsConfigs.FirstOrDefault(e => e.PartType == type);

            if (currentConfing == null)
                throw new ArgumentNullException($"Not found object type of {type}");

            if (_partsConfigs.FindAll(item => item.PartType == type).Count > 1)
            {
                ButtonView button = GetPartButton(currentConfing);
                _buttonsView.Add(button);
            }

            ButtonView button2 = GetColorButton(currentConfing);
            _buttonsView.Add(button2);
        }


        private ShopVariantButton GetShopVariant(CatPartConfig config)
        {
            var e = Instantiate(_shopVariantButtonPrefab, _buttonShopPlace);
            e.SetIcon(config.PartSprite);
            e.SetCostText(config.Cost.ToString());

            _shopButons.Add(e);
            return e;
        }

        private ShopVariantButton GetColorVariant(CatColorConfig config)
        {
            var e = Instantiate(_shopVariantButtonPrefab, _buttonShopPlace);
            e.SetColor(config.PartColor);
            e.SetCostText(config.Cost.ToString());

            _shopButons.Add(e);
            return e;
        }

        private ButtonView GetPartButton(CatPartConfig currentConfing)
        {
            ButtonView button = Instantiate(_buttonPrefab, _buttonSliderPlace);
            button.SetImage(currentConfing.PartSprite);
            button.SetType(currentConfing.PartType, false);

            return button;
        }

        private ButtonView GetColorButton(CatPartConfig currentConfing)
        {
            ButtonView button = Instantiate(_buttonPrefab, _buttonSliderPlace);
            button.SetImage(currentConfing.PartSprite);
            button.SetType(currentConfing.PartType, true);
            button.SetImageColor(new Color32(43, 236, 212, 255));

            return button;
        }
    }
}
