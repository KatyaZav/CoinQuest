using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] CoinGenerator _generator;
    [SerializeField] Bank _bank;

    [SerializeField] private GameObject _coinView;
    [SerializeField] private Coin _coin;
    [SerializeField] float _time;

    void Start()
    {
        _bank.Init();

        SubscriptionKeeper.CoinChangedEvent += OnChangeCoin;

        GenerateCoin();
    }

    private void OnDisable()
    {
        SubscriptionKeeper.CoinChangedEvent -= OnChangeCoin;
    }

    private void OnValidate()
    {
        _generator = FindAnyObjectByType<CoinGenerator>();
        _bank = FindAnyObjectByType<Bank>();
    }

    private void GenerateCoin()
    {
        _coin.gameObject.SetActive(true);

        _generator.GenerateCoin();
        _coin.SetAnimation();
    }

    private void OnChangeCoin()
    {
        _generator.GenerateCoin();

        _coinView.SetActive(true);
        _coin.gameObject.SetActive(false);
        
        Invoke("AnimateGenerateCoin", _time);
    }

    private void AnimateGenerateCoin()
    {
        _coinView.SetActive(false);
        GenerateCoin();
    }
}
