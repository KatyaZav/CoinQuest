using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bank : MonoBehaviour
{
    [SerializeField] Text _bankText;

    public void PutCoinToBank()
    {
        PlayerSaves.PutCoinsToBank();
    }

    public void Init()
    {
        SubscriptionKeeper.ChangeBankEvent += ChangeBankValue;

        ChangeBankValue();
    }

    private void ChangeBankValue()
    {
        _bankText.text = PlayerSaves.CoinsInBank.ToString();
    } 

    private void OnDisable()
    {
        SubscriptionKeeper.ChangeBankEvent -= ChangeBankValue;        
    }
}
