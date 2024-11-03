using System;
using UnityEngine;

public static class SubscriptionKeeper
{
    public static Action MoneyValueChangedEvent;
    public static Action ChangeBankEvent;
    public static Action<ItemsInfo> GettedNewEvent;

    public static void ChangeMoneyValue()
    {
        MoneyValueChangedEvent?.Invoke();
    }

    public static void ChangeBank()
    {
        ChangeBankEvent?.Invoke();
    }

    public static void GettedNew(ItemsInfo item)
    {
        GettedNewEvent?.Invoke(item);
    }
}
