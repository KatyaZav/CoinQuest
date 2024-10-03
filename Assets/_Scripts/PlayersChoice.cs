using System;
using UnityEngine;

public class PlayersChoice : MonoBehaviour
{
    public Action CoinDropedEvent, MimikGettedEvent, CoinCollectedEvent;

    [SerializeField] CoinGenerator _generator;
    [SerializeField] GameObject _yes, _no;

    public void DropCoin()
    {
        CoinDropedEvent?.Invoke();
    }

    public void CollectCoin()
    {
        if (_generator.GetIsMimik())
        {
            CoinCollectedEvent?.Invoke();
        }
        else
        {
            CoinCollectedEvent?.Invoke();
        }
    }       

    public void DisableButtons()
    {
        _yes.SetActive(false);
        _no.SetActive(false);
    }

    public void ActivateButtons()
    {
        _yes.SetActive(true);
        _no.SetActive(true);
    }

    private void OnValidate()
    {
        _generator ??= FindAnyObjectByType<CoinGenerator>(); 
    }
}
