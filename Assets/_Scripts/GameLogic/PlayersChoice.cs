using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayersChoice : MonoBehaviour
{
    public Action ItemDropedEvent, MimikGettedEvent, ItemCollectedEvent;

    [SerializeField] CoinGenerator _generator;
    [SerializeField] Button _yes, _no;

    private void Start()
    {
        _yes.onClick.AddListener(CollectCoin);
        _no.onClick.AddListener(DropCoin);
    }

    public void DropCoin()
    {
        ItemDropedEvent?.Invoke();
    }

    public void CollectCoin()
    {
        if (_generator.GetIsMimik())
        {
            MimikGettedEvent?.Invoke();
        }
        else
        {
            ItemCollectedEvent?.Invoke();
        }
    }       

    public void DisableButtons()
    {
        Activate(false);
    }

    public void ActivateButtons()
    {
        Activate(true);
    }

    void Activate(bool isTrue)
    {
        _yes.gameObject.SetActive(isTrue);
        _no.gameObject.SetActive(isTrue);
    }
}
