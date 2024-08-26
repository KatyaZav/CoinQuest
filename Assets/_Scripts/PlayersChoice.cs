using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersChoice : MonoBehaviour
{
    [SerializeField] CoinGenerator _generator;
    [SerializeField] Scrimmer _scrimmer;

    public void Init()
    {

    }

    public void DropCoin()
    {
        OnDropCoin();
        SubscriptionKeeper.ChangeCoin();
    }

    public void CollectCoin()
    {
        if (_generator.GetIsMimik())
            OnCollectMimik();
        else
            OnCollectCoin();

        SubscriptionKeeper.ChangeCoin();
    }

    private void OnDropCoin()
    {
    }

    private void OnCollectMimik()
    {
        _scrimmer.Generate();
    }

    private void OnCollectCoin()
    {
        PlayerSaves.AddCoins(_generator.GetCoinValue());
    }

    private void OnValidate()
    {
        _generator ??= FindAnyObjectByType<CoinGenerator>(); 
        _scrimmer ??= FindAnyObjectByType<Scrimmer>();

    }
}
