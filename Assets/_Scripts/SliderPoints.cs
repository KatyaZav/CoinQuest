using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPoints : MonoBehaviour
{
    [SerializeField] Bank _bank;

    [SerializeField] private Image _image;
    [SerializeField] private float _maxValue;
    private float _currentValue = 0;

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
        _image.fillAmount = _currentValue/_maxValue;

        if (_currentValue / _maxValue >= 1)
        {
            ResetValue();
            _bank.ChangePopup();
        }
    }
}
