using System;
using UnityEngine;

public static class SubscriptionKeeper
{
    public static Action MoneyValueChangedEvent;
    public static Action ChangeBankEvent;

    public static void ChangeMoneyValue()
    {
        MoneyValueChangedEvent?.Invoke();
    }

    public static void ChangeBank()
    {
        ChangeBankEvent?.Invoke();
    }
}
