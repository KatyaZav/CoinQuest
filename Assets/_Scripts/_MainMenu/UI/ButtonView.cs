using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Menu.UI
{
    public class ButtonView : MonoBehaviour
    {
        public event Action<CatPart, bool> Clicked;

        [SerializeField] private Image _iconImage;
        [SerializeField] private Button _button;

        private CatPart _config;
        private bool _isColor;

        public void SetType(CatPart type, bool isColor)
        {
            _config = type;
            _isColor = isColor;
        }
        public void SetImage(Sprite iconImage) => _iconImage.sprite = iconImage;
        public void SetImageColor(Color32 color) => _iconImage.color = color;

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
            Clicked?.Invoke(_config, _isColor);
        }
    }
}
