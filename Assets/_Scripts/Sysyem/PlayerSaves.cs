using Newtonsoft.Json;
using System.Collections.Generic;
using YG;

public static class PlayerSaves
{
    public static int CoinsInBank => YandexGame.savesData.CoinsInBank;
    public static int CoinsInPocket => YandexGame.savesData.CoinsInPocket;
    public static int CoinsInLeaderboards => YandexGame.savesData.CoinsInLeaderboard;
    public static List<ItemsData> Items => JsonConvert.DeserializeObject<List<ItemsData>>(YandexGame.savesData.ListItems);
    
    public static void MakeSeen(Items item)
    {
        ItemsData data;

        if (TryGetItemContain(item, out data))
        {
            if (data.IsSaw == true)
                return;

            data.See();
        }
        else
        {
            AddItem(item);
        }

        YandexGame.SaveProgress();
    }

    public static void MakeGetted(Items item)
    {
        ItemsData data;

        if (TryGetItemContain(item, out data))
        {
            if (data.IsGetted == true)
                return;

            SubscriptionKeeper.GettedNew(item);
            data.Get();
        }
        else
        {
            AddItem(item);
        }

        YandexGame.SaveProgress();
    }

    public static bool TryGetItemContain(Items item, out ItemsData itemData)
    {
        foreach (var e in Items)
        {
            if (e.Item == item)
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
        foreach (var item in items)
        {
            ItemsData element;

            if (TryGetItemContain(item, out element) == false)
            {
                AddItem(item);
            }
        }

        YandexGame.SaveProgress();
    }

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

    static void AddItem(Items item)
    {
        var newList = new List<ItemsData>(Items);
        newList.Add(new ItemsData(item));
        YandexGame.savesData.ListItems = JsonConvert.SerializeObject(newList, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
    }
}
