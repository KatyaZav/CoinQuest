public static class PlayerSaves
{
    public static int CoinsInBank;
    public static int CoinsInPocket;

    public static void PutCoinsToBank()
    {
        CoinsInBank += CoinsInPocket;
        CoinsInPocket = 0;
    }

    public static void AddCoins(int value)
    {
        CoinsInPocket += value;
    }

    public static void LooseCoins()
    {
        CoinsInPocket = 0;
    }
}
