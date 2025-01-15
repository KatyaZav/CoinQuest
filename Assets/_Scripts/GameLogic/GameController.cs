using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.UI;
using Random = UnityEngine.Random;
using Assets.Gameplay;
using Assets.Gameplay.UI;

public class GameController : MonoBehaviour
{
    /*[Header("Components")]    
    [SerializeField] private ItemView _coin;
    [SerializeField] private AudioClip _winSound, _dropSound;

    [Space(5), Header("Settings")]
    [SerializeField] private GameObject _coinView;
    
    [SerializeField] private float _time;
    [SerializeField] private float _timeBetweenCoinGet = 0.5f;
    [SerializeField] private float _timeBetweenScrimmers = 2;
    [SerializeField] private float _scaryProbability = 60;
    [SerializeField] private int _minScary, _maxScary;
    [SerializeField] private float _min, _max;

    [Space(5), Header("Particles")]
    [SerializeField] private ParticleSystem _winSystem;
    //[SerializeField] private ParticleSystem _loseSystem;
    
    
    
    public void Init()
    {
        _coin.Init();

        _playersChoice.ItemCollectedEvent += OnCoinCollect;
        _playersChoice.ItemDropedEvent += OnCoinDrop;
        _playersChoice.MimikGettedEvent += OnMimikGet;

        StartRound();
    }


    private void OnDisable()
    {
        _playersChoice.ItemCollectedEvent -= OnCoinCollect;
        _playersChoice.ItemDropedEvent -= OnCoinDrop;
        _playersChoice.MimikGettedEvent -= OnMimikGet;
    }

    private void OnMimikGet()
    {
        StopRound();

        int waitTime = 0;

        if (_generator.FailProbability > _scaryProbability)
            waitTime += UnityEngine.Random.Range(_minScary, _maxScary);
        else
            waitTime += (int)_timeBetweenCoinGet;

        CraryAnimationActivate(waitTime, false);

        Invoke("ActivateMimik", waitTime);
        Invoke("RemoveMimik", _timeBetweenScrimmers + waitTime);

        Invoke("StartRound", _time + _timeBetweenCoinGet + waitTime);
    }

    private void OnCoinDrop()
    {
        StopRound();
        Invoke("StartRound", _timeBetweenCoinGet);
    }

    private void OnCoinCollect()
    {
        StopRound();

        int time = 0;

        if (_generator.FailProbability > _scaryProbability)
        {
            time += UnityEngine.Random.Range(_minScary, _maxScary);
        }

        CraryAnimationActivate(time, true);

        Invoke("StartRound", _timeBetweenCoinGet + time);
    }

    private void StartRound()
    {
        print("start round");
        //_coinView.SetActive(false);

        GenerateCoin();

        //_coin.SetAnimation();
        _playersChoice.ActivateButtons();
    }
    private void StopRound()
    {
        _playersChoice.DisableButtons();

        _coinView.SetActive(true);
        _coin.gameObject.SetActive(false);
    }
    private void GenerateCoin()
    {
        _coin.gameObject.SetActive(true);
        _generator.GenerateItem();
        //_coin.SetAnimation();
    }

    private void CraryAnimationActivate(int waitTime, bool success)
    {
        if (success)
        {
            Invoke("AddCoins", waitTime);
        }
        else
        {
            Invoke("LooseCoins", waitTime);
        }
        //throw new NotImplementedException();
    }

    void AddCoins()
    {
        _sliderPoints.AddValue();
        _generator.GenerateItem();

        PlayerSaves.AddCoins(_generator.CoinValue);
     //   _winSystem.Play();
    }

    void LooseCoins()
    {
        PlayerSaves.LooseCoins();
        //_loseSystem.Play();
    }*/
}
