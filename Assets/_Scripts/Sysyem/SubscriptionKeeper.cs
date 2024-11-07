using System;
using UnityEngine;

public static class SubscriptionKeeper
{
    public static Action MoneyValueChangedEvent;
    public static Action ChangeBankEvent;
    public static Action<Items> GettedNewEvent;

    public static void ChangeMoneyValue()
    {
        MoneyValueChangedEvent?.Invoke();
    }

    public static void ChangeBank()
    {
        ChangeBankEvent?.Invoke();
    }

    public static void GettedNew(Items item)
    {
        Debug.Log("Find new" + item.name);
        GettedNewEvent?.Invoke(item);
    }
}
