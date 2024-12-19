using System;
using System.Diagnostics;
using System.Linq;
using YG;

public static class PlayerSaves
{
    public static Action<int> ChangedCountScrimmers;
    public static Action<int, int> ChangedFinalCountMoney;

    public static int CoinsInBank => YandexGame.savesData.CoinsInBank;
    public static int CoinsInPocket => YandexGame.savesData.CoinsInPocket;
    public static int CoinsInLeaderboards => YandexGame.savesData.CoinsInLeaderboard;
    public static int PreviousCount => YandexGame.savesData.PreviousCount;

    public static int GettedScrimmerCount => YandexGame.savesData.GettedScrimmersID.Count;

    public static bool CheakIsScrimmerGetted(int id) => 
        YandexGame.savesData.GettedScrimmersID
        //.DefaultIfEmpty(-1)
        .FirstOrDefault(item => item == id)
        != 0; 

    public static void AddScrimmer(int id)
    {
        if (CheakIsScrimmerGetted(id))
            throw new System.ArgumentException($"Already contain {id} item");

        //Debug.Log("added new scrimmer");

        YandexGame.savesData.GettedScrimmersID.Add(id);
        YandexGame.SaveProgress();

        ChangedCountScrimmers?.Invoke(
            YandexGame.savesData.GettedScrimmersID.Count);
    }

    public static void PutCoinsToBank()
    {
        YandexGame.savesData.PreviousCount = YandexGame.savesData.CoinsInBank;

        YandexGame.savesData.CoinsInBank += CoinsInPocket;
        YandexGame.savesData.CoinsInPocket = 0;

        SubscriptionKeeper.ChangeBank();
        SubscriptionKeeper.ChangeMoneyValue();

        CheackLeaderBoard();
        YandexGame.SaveProgress();

        ChangedFinalCountMoney.Invoke(PreviousCount, CoinsInBank);
    }

    public static void AddCoins(int value)
    {
        YandexGame.savesData.CoinsInPocket += value;

        SubscriptionKeeper.ChangeMoneyValue();
        //CheackLeaderBoard();

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
        int count = CoinsInBank;

        if (CoinsInLeaderboards < count)
        {
            YandexGame.savesData.CoinsInLeaderboard = count;
            YG.YandexGame.NewLeaderboardScores("leaderboard", count);
        }
    }
}
