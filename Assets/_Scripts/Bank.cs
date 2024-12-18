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
    private bool _canFree = false;

    public void FreeBank()
    {
        _canFree = false;
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
                _canFree = true;
                _currentPopup = _freeButton;
                break;
            case 2:
                _currentPopup = _adButton;
                break;
            case 3:
                _currentPopup = _timerPopup;
                break;
        }

        if (a == 4)
            _currentPopup = null;
        else
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
        SubscriptionKeeper.MoneyValueChangedEvent += Cheack;

        ChangeBankValue();
        ChangePopup(2);
        Cheack();
    }

    private void Cheack()
    {
        if (PlayerSaves.CoinsInPocket == 0)
        {
            ChangePopup(4);
        }
        else
        {
            if (_canFree)
                ChangePopup(1);
            else
            {
                if (_canAd)
                    ChangePopup(2);
                else
                    ChangePopup(3);
            }
        }
    }

    private void ChangeBankValue()
    {
        _bankText.text = PlayerSaves.CoinsInBank.ToString();
    } 

    private void OnDisable()
    {
        SubscriptionKeeper.ChangeBankEvent -= ChangeBankValue;
        YG.YandexGame.RewardVideoEvent -= PutCoinToBank;
        SubscriptionKeeper.MoneyValueChangedEvent -= Cheack;
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
