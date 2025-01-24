using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Gameplay;

namespace Events
{
    public class EventsFabric
    {
        #region ProbabilityPaths
        private const string ChangeProbabilityEventDataPath = "Events/Probability/1_CatsMoreVigilance";
        private const string ChangeProbabilityEventData2Path = "Events/Probability/2_CatsMoreVigilance";
        private const string ChangeProbabilityEventData3Path = "Events/Probability/3_CatsMoreVigilance";
        private const string ChangeProbabilityEventData4Path = "Events/Probability/4_CatsMoreVigilance";
        private const string ChangeProbabilityEventData5Path = "Events/Probability/5_CatsMoreVigilance";
        #endregion

        #region AddPawPath
        private const string AddPawPath1 = "Events/AddPaw/AddPaw 1";
        private const string AddPawPath2 = "Events/AddPaw/AddPaw 2";
        private const string AddPawPath3 = "Events/AddPaw/AddPaw 3";
        private const string AddPawPath4 = "Events/AddPaw/AddPaw 4";
        #endregion

        #region BonusCollectionPath
        private const string BonusCollectionPath = "Events/BonusPawsForCollect/Bonus 1";
        private const string BonusCollectionPath2 = "Events/BonusPawsForCollect/Bonus 2";
        private const string BonusCollectionPath3 = "Events/BonusPawsForCollect/Bonus 3";
        #endregion

        #region SecretsPath
        private const string SecretImageEventPath1 = "Events/Secrets/SecretImage";
        private const string SecretTextEventPath1 = "Events/Secrets/TextSecretEvent";
        
        private const string SecretImagePath1 = "Images/SecretImage";
        #endregion

        #region FreeButtonPaths
        private const string FreeButtonEventPath = "Events/Bank/BankFreeButton";
        #endregion

        #region AdRewardEventPath
        private const string AdRewardEventPath = "Events/Ad/AdReward";
        #endregion

        private const string ChangeRoomDataPath = "Events/ChangeRoom/ChangeRoomEventData";

        public List<IEvent> GetFullEventsList()
        {
            List<IEvent> events = new List<IEvent>();

            ItemGenerator coin = GetScriptFromScene<GameCycle>().CurrentItemGenerator;
            Bank bank = GetScriptFromScene<Bank>();

            #region Probability
            events.Add(new ChangeProbabilityEvent(coin, 20, LoadEventData(ChangeProbabilityEventDataPath)));
            events.Add(new ChangeProbabilityEvent(coin, -20, LoadEventData(ChangeProbabilityEventData2Path)));
            events.Add(new ChangeProbabilityEvent(coin, -50, LoadEventData(ChangeProbabilityEventData3Path)));
            events.Add(new ChangeProbabilityEvent(coin, 50, LoadEventData(ChangeProbabilityEventData4Path)));
            events.Add(new ChangeProbabilityEvent(coin, -80, LoadEventData(ChangeProbabilityEventData5Path)));
            #endregion

            #region AddPaw
            events.Add(new AddPawsEvent(LoadEventData(AddPawPath1), 2));
            events.Add(new AddPawsEvent(LoadEventData(AddPawPath2), 3));
            events.Add(new AddPawsEvent(LoadEventData(AddPawPath3), 5));
            events.Add(new AddPawsEvent(LoadEventData(AddPawPath4), 1));
            #endregion

            #region BonusCollection
            events.Add(new BonusCollectEvent(LoadEventData(BonusCollectionPath), 2, coin));
            events.Add(new BonusCollectEvent(LoadEventData(BonusCollectionPath2), 3, coin));
            #endregion

            #region Secret
            Sprite secretSprite = LoadData<Sprite>(SecretImagePath1);
            EventData data = LoadEventData(SecretTextEventPath1);

            events.Add(new MakeImageSecretEvent(coin, LoadEventData(SecretImageEventPath1), secretSprite));

            events.Add(new SecretTextEvent(coin, data, -3, "?"));
            events.Add(new SecretTextEvent(coin, data, -5, "?"));
            events.Add(new SecretTextEvent(coin, data, -7, "??"));
            events.Add(new SecretTextEvent(coin, data, -12, "???"));
            #endregion

            #region FreeButton
            EventData freeButtonData = LoadEventData(FreeButtonEventPath);

            events.Add(new AddFreeButtonEvent(freeButtonData, bank, 1));
            events.Add(new AddFreeButtonEvent(freeButtonData, bank, 2));
            events.Add(new AddFreeButtonEvent(freeButtonData, bank, 3));
            events.Add(new AddFreeButtonEvent(freeButtonData, bank, 4));
            #endregion

            #region AdReward

            EventData AdRewardData = LoadEventData(AdRewardEventPath);

            events.Add(new RewardBankEvent(AdRewardData, 5));
            #endregion

            EventData changeRoomData = LoadEventData(ChangeRoomDataPath);
            events.Add(new ChangeRoomEvent(coin, changeRoomData));
            events.Add(new ChangeRoomEvent(coin, changeRoomData));
            events.Add(new ChangeRoomEvent(coin, changeRoomData));
            events.Add(new ChangeRoomEvent(coin, changeRoomData));

            return events;
        }

        private T LoadData<T>(string path) where T : UnityEngine.Object
        {
            var item = Resources.Load<T>(path);

            if (item == null)
                throw new NullReferenceException($"Not found item {typeof(T)} in path {path}");

            return item;
        }

        private EventData LoadEventData(string path)
            => LoadData<EventData>(path);

        private T GetScriptFromScene<T>() where T : UnityEngine.Object
        {
            var script = GameObject.FindFirstObjectByType<T>();

            if (script == null)
                throw new System.ArgumentNullException($"Not found object type {typeof(T)} in scene!");

            return script;
        }
    }
}
