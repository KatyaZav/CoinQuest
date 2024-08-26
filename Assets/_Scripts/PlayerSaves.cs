public static class PlayerSaves
{
    public static int CoinsInBank;
    public static int CoinsInPocket;

    public static void PutCoinsToBank()
    {
        CoinsInBank += CoinsInPocket;
        CoinsInPocket = 0;

        SubscriptionKeeper.ChangeBank();
        SubscriptionKeeper.ChangeMoneyValue();
    }

    public static void AddCoins(int value)
    {
        CoinsInPocket += value;

        SubscriptionKeeper.ChangeMoneyValue();
    }

    public static void RemoveCoins(int value)
    {
        CoinsInPocket -= value;

        if (CoinsInPocket < 0)
        {
            CoinsInPocket = 0;
        }

        SubscriptionKeeper.ChangeMoneyValue();
    }

    public static void LooseCoins()
    {
        CoinsInPocket = 0;

        SubscriptionKeeper.ChangeMoneyValue();
    }
}
