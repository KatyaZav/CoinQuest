using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] CoinGenerator _generator;
    [SerializeField] Bank _bank;
    void Start()
    {
        _generator.Init();
        _bank.Init();
    }

    private void OnValidate()
    {
        _generator = FindAnyObjectByType<CoinGenerator>();
        _bank = FindAnyObjectByType<Bank>();
    }
}
