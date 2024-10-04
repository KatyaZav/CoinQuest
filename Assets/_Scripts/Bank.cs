using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bank : MonoBehaviour
{
    [SerializeField] Text _bankText;
    [SerializeField] GameObject _adButton, _timerPopup,  _freeButton;

    [SerializeField] Text _timerText;
    [SerializeField] int _time;

    private GameObject _currentPopup;
    private bool _canAd = true;

    public void FreeBank()
    {
        PlayerSaves.PutCoinsToBank();

        if (_canAd)
            ChangePopup(2);
        else
            ChangePopup(3);
    }

    public void ChangePopup(int a = 1)
    {
        if (_currentPopup != null) 
            _currentPopup.SetActive(false);

        switch (a)
        {
            case 1:                
                _currentPopup = _freeButton;
                break;
            case 2:
                _currentPopup = _adButton;
                break;
            case 3:
                _currentPopup = _timerPopup;
                break;
        }

        _currentPopup.SetActive(true);
    }

    public void PutCoinToBank(int a)
    {
        PlayerSaves.PutCoinsToBank();

        StartCoroutine(TimerLogic());
    }

    public void Init()
    {
        _canAd = true;

        YG.YandexGame.RewardVideoEvent += PutCoinToBank;
        SubscriptionKeeper.ChangeBankEvent += ChangeBankValue;      

        ChangeBankValue();
        ChangePopup(2);
    }

    private void ChangeBankValue()
    {
        _bankText.text = PlayerSaves.CoinsInBank.ToString();
    } 

    private void OnDisable()
    {
        SubscriptionKeeper.ChangeBankEvent -= ChangeBankValue;
        YG.YandexGame.RewardVideoEvent -= PutCoinToBank;
    }

    private IEnumerator TimerLogic()
    {
        ChangePopup(3);
        _canAd = false;

        int u = _time;
        while (u > 0)
        {
            yield return new WaitForSeconds(1);

            u--;

            int minites = u / 60;
            int seconds = u % 60;

            _timerText.text = $"{minites}:{seconds}"; 
        }

        _canAd = true;
        if (_freeButton.activeSelf == false)
            ChangePopup(2);
    }
}
