using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Menu.UI
{
    public class ButtonView : MonoBehaviour
    {
        public event Action Clicked;

        [SerializeField] private Image _iconImage;
        [SerializeField] private Button _button;

        public void SetImage(Image iconImage) => _iconImage = iconImage;

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
