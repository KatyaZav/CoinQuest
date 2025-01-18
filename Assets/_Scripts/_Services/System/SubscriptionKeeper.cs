using System;
using UnityEngine;

public static class SubscriptionKeeper
{
    public static Action<Items> GettedNewEvent;
    public static event Action<Action> MimikActivated;

    public static void GettedNew(Items item)
    {
        Debug.Log("Find new " + item.name);
        GettedNewEvent?.Invoke(item);
    }

    public static void MimikActivate(Action callback)
    {
        MimikActivated?.Invoke(callback);
    }
}
