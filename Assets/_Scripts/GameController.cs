using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] CoinGenerator _generator;
    [SerializeField] Bank _bank;
    void Start()
    {
        _bank.Init();

        SubscriptionKeeper.CoinChangedEvent += OnChangeCoin;
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
    private void OnChangeCoin()
    {
        _generator.GenerateCoin();
    }
}
