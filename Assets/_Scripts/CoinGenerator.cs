using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField, Range(1,100)] private int _minValue, _maxValue;

    public float GetMimikProbability() => _coin.Probability;
    public int GetCoinValue() => _coin.Value;
    public bool GetIsMimik() => _coin.IsMimic;

    
    public void GenerateCoin()
    {
        _coin.GenerateCoin(_minValue, _maxValue);
    }
    
    private void ChangeCoin()
    {
        GenerateCoin();
    }

    private void OnValidate()
    {
        _coin = FindAnyObjectByType<Coin>();

        if (_coin == null )
            Debug.LogWarning("Coin not found");

        if (_maxValue <= _minValue)
        {
            Debug.LogWarning("Max value lower min value");
            _maxValue = _minValue+1;
        }
    }
}
