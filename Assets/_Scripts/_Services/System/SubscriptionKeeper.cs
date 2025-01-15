using System;
using UnityEngine;

public static class SubscriptionKeeper
{
    public static Action<Items> GettedNewEvent;
    public static event Action MimikActivated, MimikClosed;

    public static void GettedNew(Items item)
    {
        Debug.Log("Find new " + item.name);
        GettedNewEvent?.Invoke(item);
    }

    public static void MimikActivate()
    {
        MimikActivated?.Invoke();
    }

    public static void MimikClose()
    {
        MimikClosed?.Invoke();
    }
}
