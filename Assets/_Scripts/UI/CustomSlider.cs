using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI
{
    public class CustomSlider
    {
        public Action OnSliderEndEvent;

        [SerializeField] private Image _image;
        [SerializeField] private float _maxValue;
        private float _currentValue = 0;

        public CustomSlider(Image image, float maxValue)
        {
            _image = image;
            _maxValue = maxValue;
            _currentValue = 0;
        }

        public void ResetValue()
        {
            _currentValue = 0;
            SetValue(0);
        }

        public void SetValue(float a)
        {
            _image.fillAmount = a;
        }

        public void AddValue(float a = 1)
        {
            _currentValue += a;
            _image.fillAmount = _currentValue / _maxValue;

            if (_currentValue / _maxValue >= 1)
            {
                ResetValue();
                OnSliderEndEvent?.Invoke();
            }
        }
    }
}
