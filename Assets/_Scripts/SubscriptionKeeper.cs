using System;
using UnityEngine;

public static class SubscriptionKeeper
{
    public static Action<bool> CoinChanged;

    public static void ChangeCoinWithDropEvent(bool isDrop)
    {
        CoinChanged?.Invoke(isDrop);
    } 
}
