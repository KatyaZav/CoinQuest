using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] CoinGenerator _generator;
    
    void Start()
    {
        _generator.Init();
    }

    private void OnValidate()
    {
        _generator = FindAnyObjectByType<CoinGenerator>();
    }
}
