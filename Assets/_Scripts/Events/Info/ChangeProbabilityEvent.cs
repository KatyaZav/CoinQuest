using UnityEngine;

namespace Events
{
    public class ChangeProbabilityEvent : IEvent
    {
        private Coin _coin;
        private EventData _data;

        private int _probability;

        public ChangeProbabilityEvent(Coin coin, int probability, EventData data)
        {
            _coin = coin;
            _data = data;
            _probability = probability;
        }

        public EventData EventData => _data;

        public void Enter()
        {
            Debug.Log("Activate event");
            _coin.ChangeExtraProbability(_probability);
        }

        public void Exit()
        {
            Debug.Log("Disactivate event");
            _coin.ChangeExtraProbability(_probability * -1);
        }
    }
}