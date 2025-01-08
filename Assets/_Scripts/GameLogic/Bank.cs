using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class Bank : MonoBehaviour
{
    [SerializeField] Text _bankText;
    [SerializeField] GameObject _adButton, _timerPopup,  _freeButton;
    [SerializeField] Text _freeText;

    [SerializeField] Text _timerText;
    [SerializeField] int _time;

    private GameObject _currentPopup;
    private bool _canAd = true;
    private int _freeButtonCount => PlayerSaves.ButtonCount.Value;

    public void AddFreeButton(int count)
    {
        if (count < 0)
            throw new ArgumentException($"Cant add {count} free points");

        PlayerSaves.AddButtonCount(count);
        //_freeButtonCount += count;
        Cheack(PlayerSaves.CoinsInPocket.Value);

        _freeText.text = _freeButtonCount.ToString();
    }

    public void FreeBank()
    {
        if (_freeButtonCount <= 0)
            throw new ArgumentException("Cant activate free button");

        PlayerSaves.RemoveButtonCount(1);
        //_freeButtonCount -= 1;
        PlayerSaves.PutCoinsToBank();

        if (_freeButtonCount <= 0)
        {
            if (_canAd)
                ChangePopup(2);
            else
                ChangePopup(3);
        }
    }

    public void ChangePopup(int a = 1)
    {
        if (_currentPopup != null) 
            _currentPopup.SetActive(false);

        switch (a)
        {
            case 1:
                _currentPopup = _freeButton;
                _freeText.text = _freeButtonCount.ToString();
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
        PlayerSaves.CoinsInBank.Changed += ChangeBankValue;
        PlayerSaves.CoinsInPocket.Changed += Cheack;

        ChangeBankValue(PlayerSaves.CoinsInBank.Value);
        ChangePopup(2);
        Cheack(PlayerSaves.CoinsInPocket.Value);
    }

    private void Cheack(int value)
    {
        if (value == 0)
        {
            ChangePopup(4);
        }
        else
        {
            if (_freeButtonCount > 0)
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

    private void ChangeBankValue(int value)
    {
        _bankText.text = value.ToString();
    } 

    private void OnDisable()
    {
        YG.YandexGame.RewardVideoEvent -= PutCoinToBank;

        PlayerSaves.CoinsInBank.Changed += ChangeBankValue;
        PlayerSaves.CoinsInPocket.Changed += Cheack;
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
