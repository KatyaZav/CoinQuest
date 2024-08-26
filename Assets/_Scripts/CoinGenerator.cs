using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] Coin _coin;
    [SerializeField, Range(1,100)] int _minValue, _maxValue;

    public void Init()
    {
        SubscriptionKeeper.CoinChangedEvent += ChangeCoin;

        GenerateCoin();
    }

    public int GetCoinValue() => _coin.Value;
    public bool GetIsMimik() => _coin.IsMimic;

    private void OnDisable()
    {
        SubscriptionKeeper.CoinChangedEvent -= ChangeCoin;

    }
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
