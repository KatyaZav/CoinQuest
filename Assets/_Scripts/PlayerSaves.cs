using YG;

public static class PlayerSaves
{
    public static int CoinsInBank => YandexGame.savesData.CoinsInBank;
    public static int CoinsInPocket => YandexGame.savesData.CoinsInPocket;
    public static int CoinsInLeaderboards => YandexGame.savesData.CoinsInLeaderboard;
    
    public static void PutCoinsToBank()
    {
        YandexGame.savesData.CoinsInBank += CoinsInPocket;
        YandexGame.savesData.CoinsInPocket = 0;

        SubscriptionKeeper.ChangeBank();
        SubscriptionKeeper.ChangeMoneyValue();

        CheackLeaderBoard();
        YandexGame.SaveProgress();
    }

    public static void AddCoins(int value)
    {
        YandexGame.savesData.CoinsInPocket += value;

        SubscriptionKeeper.ChangeMoneyValue();
        CheackLeaderBoard();

        YandexGame.SaveProgress();
    }    

    public static void RemoveCoins(int value)
    {
        YandexGame.savesData.CoinsInPocket -= value;

        if (CoinsInPocket < 0)
        {
            YandexGame.savesData.CoinsInPocket = 0;
        }

        SubscriptionKeeper.ChangeMoneyValue();
        YandexGame.SaveProgress();
    }

    public static void LooseCoins()
    {
        YandexGame.savesData.CoinsInPocket = 0;

        SubscriptionKeeper.ChangeMoneyValue();
        YandexGame.SaveProgress();
    }

    private static void CheackLeaderBoard()
    {
        int count = CoinsInBank + CoinsInPocket;

        if (CoinsInLeaderboards < count)
        {
            YandexGame.savesData.CoinsInLeaderboard = count;
            YG.YandexGame.NewLeaderboardScores("leaderboard", count);
        }
    }
}
