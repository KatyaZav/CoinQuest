using System.Collections.Generic;
using UnityEngine;
using System; 

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

        #region SecretsPaths
        private const string SecretImageEventPath1 = "Events/Secrets/SecretImage";
        private const string SecretTextEventPath1 = "Events/Secrets/TextSecretEvent";
        
        private const string SecretImagePath1 = "Images/SecretImage";

        #endregion

        public List<IEvent> GetFullEventsList()
        {
            List<IEvent> events = new List<IEvent>();

            Coin coin = GetCoin();

            /*#region Probability
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
            events.Add(new BonusCollectEvent(LoadEventData(BonusCollectionPath3), 0.5f, coin));
            #endregion*/

            #region Secret
            Sprite secretSprite = LoadData<Sprite>(SecretImagePath1);

            events.Add(new MakeImageSecretEvent(coin, LoadEventData(SecretImageEventPath1), secretSprite));

            events.Add(new SecretTextEvent(coin, LoadEventData(SecretTextEventPath1), -3, "?"));
            events.Add(new SecretTextEvent(coin, LoadEventData(SecretTextEventPath1), -5, "?"));
            events.Add(new SecretTextEvent(coin, LoadEventData(SecretTextEventPath1), -7, "??"));
            events.Add(new SecretTextEvent(coin, LoadEventData(SecretTextEventPath1), -12, "???"));
            #endregion

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

        private Coin GetCoin()
        {
            var _coins = GameObject.FindFirstObjectByType<Coin>();

            if (_coins == null)
                throw new System.ArgumentNullException("Not found coin in scene!");

            return _coins;
        }
    }
}
