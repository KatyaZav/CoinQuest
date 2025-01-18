using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Menu.Shop
{
    public class ShopVariantButton : MonoBehaviour
    {
        public event Action Clicked;

        [SerializeField] private Button _button;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _statusImage;

        [SerializeField] private GameObject _buyingInfoPanel;
        [SerializeField] private Text _costText;

        public void SetIcon(Sprite image) => _iconImage.sprite = image;
        public void SetStatusIcon(Sprite image) => _statusImage.sprite = image;
        public void SetCostText(string text) => _costText.text = text;
        public void MakePanelActive(bool isActive) => _buyingInfoPanel.SetActive(isActive);

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);   
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);  
        }

        private void OnClick()
        {
            Clicked?.Invoke();
        }
    }
}
