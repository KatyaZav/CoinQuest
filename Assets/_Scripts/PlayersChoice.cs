using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersChoice : MonoBehaviour
{
    public void Init()
    {

    }

    private void OnDisable()
    {
        
    }

    public void DropCoin()
    {
        SubscriptionKeeper.ChangeCoinWithDropEvent(true);
    }

    public void CollectCoin()
    {
        SubscriptionKeeper.ChangeCoinWithDropEvent(false);
    }

    private void OnDropCoin()
    {

    }

    private void OnCollectCoin()
    {

    }
}
