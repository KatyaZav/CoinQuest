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

    private const string ActivateButtonName = "ActivateButtons"; 

    public void Init()
    {

    }

    public void DropCoin()
    {
        ButtonSwitch(_timeBetween);
        //OnDropCoin();
    }

    public void CollectCoin()
    {
        ActivateCollectCoinAnimation();

        if (_generator.GetIsMimik())
        {
            int timeBeforeScrimmer = Random.Range(1, 3);

            ButtonSwitch(_timeBetweenScrimmers + timeBeforeScrimmer);
            Invoke("OnCollectMimik", timeBeforeScrimmer);
        }
        else
        {
            float time = _timeBetween;

            if (_generator.GetMimikProbability() > 50)
            {
                int timeBeforeScrimmer = Random.Range(1, 3);

                time += timeBeforeScrimmer;
            }

            ButtonSwitch(time);    
            Invoke("OnCollectCoin", time);
        }
    }

    private void ActivateCollectCoinAnimation()
    {

    }

    private void ButtonSwitch(float time)
    {
        DisableButtons();
        Invoke(ActivateButtonName, time);

        SubscriptionKeeper.ChangeCoin();
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
