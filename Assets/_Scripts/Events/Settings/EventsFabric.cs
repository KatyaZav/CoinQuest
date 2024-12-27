using System.Collections.Generic;
using UnityEngine;

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

        public List<IEvent> GetFullEventsList()
        {
            List<IEvent> events = new List<IEvent>();

            #region Probability
            events.Add(new ChangeProbabilityEvent(GetCoin(), 20, LoadData(ChangeProbabilityEventDataPath)));
            events.Add(new ChangeProbabilityEvent(GetCoin(), -20, LoadData(ChangeProbabilityEventData2Path)));
            events.Add(new ChangeProbabilityEvent(GetCoin(), -50, LoadData(ChangeProbabilityEventData3Path)));
            events.Add(new ChangeProbabilityEvent(GetCoin(), 50, LoadData(ChangeProbabilityEventData4Path)));
            events.Add(new ChangeProbabilityEvent(GetCoin(), -80, LoadData(ChangeProbabilityEventData5Path)));
            #endregion

            #region AddPaw
            events.Add(new AddPawsEvent(LoadData(AddPawPath1), 2));
            events.Add(new AddPawsEvent(LoadData(AddPawPath2), 3));
            events.Add(new AddPawsEvent(LoadData(AddPawPath3), 5));
            events.Add(new AddPawsEvent(LoadData(AddPawPath4), 1));
            #endregion

            #region BonusCollection
            events.Add(new BonusCollectEvent(LoadData(BonusCollectionPath), 2, GetCoin()));
            events.Add(new BonusCollectEvent(LoadData(BonusCollectionPath2), 3, GetCoin()));
            events.Add(new BonusCollectEvent(LoadData(BonusCollectionPath3), 0.5f, GetCoin()));
            #endregion

            return events;
        }

        private EventData LoadData(string path)
            => Resources.Load<EventData>(path);

        private Coin GetCoin()
        {
            var _coins = GameObject.FindFirstObjectByType<Coin>();

            if (_coins == null)
                throw new System.ArgumentNullException("Not found coin in scene!");

            return _coins;
        }
    }
}
