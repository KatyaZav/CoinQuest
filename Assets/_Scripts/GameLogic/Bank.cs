using Assets.UI;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bank : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] Text _bankText;
    [SerializeField] Text _freeText;
    [SerializeField] Text _timerText;

    [Header("Settings")]
    [SerializeField] int _time;
    [SerializeField] GameObject _adButton, _timerPopup,  _freeButton;

    private GameObject _currentPopup;
    private bool _canAd = true;

    private ReactiveUI<int> _bankCoinUi, _freeButtonCountUI;

    public void Init()
    {
        _bankCoinUi = new ReactiveUI<int>(PlayerSaves.CoinsInBank, _bankText);
        _freeButtonCountUI = new ReactiveUI<int>(PlayerSaves.ButtonCount, _freeText);

        _bankCoinUi.Init();
        _freeButtonCountUI.Init();

        _canAd = true;

        YG.YandexGame.RewardVideoEvent += PutCoinToBank;
        PlayerSaves.CoinsInPocket.Changed += Cheack;

        ChangePopup(2);
        Cheack(PlayerSaves.CoinsInPocket.Value);
    }

    public void AddFreeButton(int count)
    {
        if (count < 0)
            throw new ArgumentException($"Cant add {count} free points");

        PlayerSaves.AddButtonCount(count);
        Cheack(PlayerSaves.CoinsInPocket.Value);
    }

    public void FreeBank()
    {
        if (PlayerSaves.ButtonCount.Value <= 0)
            throw new ArgumentException("Cant activate free button");

        PlayerSaves.RemoveButtonCount(1);
        PlayerSaves.PutCoinsToBank();

        if (PlayerSaves.ButtonCount.Value <= 0)
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
                break;
            case 2:
                _currentPopup = _adButton;
                break;
            case 3:
                _currentPopup = _timerPopup;
                break;
            case 4:
                _currentPopup = null;
                break;
        }

        _currentPopup?.SetActive(true);
    }

    public void PutCoinToBank(int a)
    {
        PlayerSaves.PutCoinsToBank();

        StartCoroutine(TimerLogic());
    }

    private void Cheack(int value)
    {
        if (value == 0)
        {
            ChangePopup(4);
        }
        else
        {
            if (PlayerSaves.ButtonCount.Value > 0)
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

    private void OnDisable()
    {
        YG.YandexGame.RewardVideoEvent -= PutCoinToBank;
        PlayerSaves.CoinsInPocket.Changed -= Cheack;

        _bankCoinUi.Dispose();
        _freeButtonCountUI.Dispose();
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
