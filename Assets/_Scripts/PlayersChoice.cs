using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersChoice : MonoBehaviour
{
    [SerializeField] CoinGenerator _generator;
    [SerializeField] Scrimmer _scrimmer;
    [SerializeField] GameObject _yes, _no;
    [SerializeField] float _timeBetween;
    [SerializeField] float _timeBetweenScrimmers;

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
        {
            Invoke("ActivateButtons", _timeBetweenScrimmers);
            OnCollectMimik();
        }
        else
        {
            Invoke("ActivateButtons", _timeBetween);    
            OnCollectCoin();
        }

        SubscriptionKeeper.ChangeCoin();
        DisableButtons();
    }

    void DisableButtons()
    {
        _yes.SetActive(false);
        _no.SetActive(false);
    }

    void ActivateButtons()
    {
        _yes.SetActive(true);
        _no.SetActive(true);
    }

    private void OnDropCoin()
    {
    }

    private void OnCollectMimik()
    {
        _scrimmer.Activate();
        Invoke("Remove", _timeBetweenScrimmers);
    }

    public void Remove()
    {
        _scrimmer.Remove();
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
