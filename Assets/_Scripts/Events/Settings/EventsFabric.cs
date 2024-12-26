using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class EventsFabric
    {
        private const string ChangeProbabilityEventDataPath = "Events/1_CatsMoreVigilance";

        public List<IEvent> GetFullEventsList()
        {
            List<IEvent> events = new List<IEvent>();

            events.Add(new ChangeProbabilityEvent(GetCoin(), 20, LoadData(ChangeProbabilityEventDataPath)));

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
