using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class EventsFabric
    {
        private const string ChangeProbabilityEventDataPath = "Events/1_CatsMoreVigilance";
        private const string ChangeProbabilityEventData2Path = "Events/2_CatsMoreVigilance";
        private const string ChangeProbabilityEventData3Path = "Events/3_CatsMoreVigilance";
        private const string ChangeProbabilityEventData4Path = "Events/4_CatsMoreVigilance";
        private const string ChangeProbabilityEventData5Path = "Events/5_CatsMoreVigilance";

        public List<IEvent> GetFullEventsList()
        {
            List<IEvent> events = new List<IEvent>();

            events.Add(new ChangeProbabilityEvent(GetCoin(), 20, LoadData(ChangeProbabilityEventDataPath)));
            events.Add(new ChangeProbabilityEvent(GetCoin(), -20, LoadData(ChangeProbabilityEventData2Path)));
            events.Add(new ChangeProbabilityEvent(GetCoin(), -50, LoadData(ChangeProbabilityEventData3Path)));
            events.Add(new ChangeProbabilityEvent(GetCoin(), 50, LoadData(ChangeProbabilityEventData4Path)));
            events.Add(new ChangeProbabilityEvent(GetCoin(), -80, LoadData(ChangeProbabilityEventData5Path)));

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
