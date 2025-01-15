using Assets.Gameplay;

namespace Events
{
    public class ChangeProbabilityEvent : IEvent
    {
        private ItemGenerator _coin;
        private EventData _data;

        private int _probability;

        public ChangeProbabilityEvent(ItemGenerator coin, int probability, EventData data)
        {
            _coin = coin;
            _data = data;
            _probability = probability;
        }

        public EventData EventData => _data;

        public void Enter()
        {
            _coin.ChangeExtraProbability(_probability);
        }

        public void Exit()
        {
            _coin.ChangeExtraProbability(_probability * -1);
        }
    }
}
