using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private Coin _coin;           

    public void GetCoin()
    {
        _coin.GetCoin();
    }

    public float GetMimikProbability() => _coin.Probability;
    public int GetCoinValue() => _coin.Value;
    public bool GetIsMimik() => _coin.IsMimic;
        
    public void GenerateCoin()
    {
        _coin.GenerateCoin();
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
    }
}
