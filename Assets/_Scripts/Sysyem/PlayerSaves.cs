using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using YG;

public static class PlayerSaves
{
    public static int CoinsInBank => YandexGame.savesData.CoinsInBank;
    public static int CoinsInPocket => YandexGame.savesData.CoinsInPocket;
    public static int CoinsInLeaderboards => YandexGame.savesData.CoinsInLeaderboard;
    public static List<ItemsData> Items =>
        JsonConvert.DeserializeObject<List<ItemsData>>(YandexGame.savesData.ListItems);
    
    public static void MakeSeen(Items item)
    {
        var list = Items;

        foreach (var e in list)
        {
            if (e.ID == item.ID)
            {
                e.See();
                break;
            }
        }

        YandexGame.savesData.ListItems = JsonConvert.SerializeObject(list);
        YandexGame.SaveProgress();
    }

    public static void MakeGetted(Items item)
    {
        var list = Items;

        foreach (var e in list)
        {
            if (e.ID == item.ID)
            {
                if (e.IsGetted == false)
                {
                    e.Get();
                    SubscriptionKeeper.GettedNew(item);
                }
                break;
            }
        }

        YandexGame.savesData.ListItems = JsonConvert.SerializeObject(list);
        YandexGame.SaveProgress();
    }

    public static bool TryGetItemContain(Items item, out ItemsData itemData)
    {
        foreach (var e in Items)
        {
            if (e.ID == item.ID)
            {
                itemData = e;
                return true;
            }
        }

        itemData = null;
        return false;
    }

    public static void UpdateList(List<Items> items)
    {
        Debug.Log("Start updatings list");

        foreach (var item in items)
        {
            if (TryGetItemContain(item, out var element) == false)
            {
                AddItem(item);
                YandexGame.SaveLocal();
            }
        }

        YandexGame.SaveProgress();
    }

    #region Coins
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
    #endregion

    static void AddItem(Items item)
    {
        Debug.Log("Add new item");
        var newList = Items;
        newList.Add(new ItemsData(item.ID));

        YandexGame.savesData.ListItems = JsonConvert.SerializeObject(newList);
    }
}
