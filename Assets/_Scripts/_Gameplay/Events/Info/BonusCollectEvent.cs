namespace Events
{
    public class BonusCollectEvent : IEvent
    {
        private EventData _data;
        private float _modifier;
        private Coin _coin;

        public BonusCollectEvent(EventData data, float modifier, Coin coin)
        {
            _data = data;
            _modifier = modifier;
            _coin = coin;
        }

        public EventData EventData => _data;

        public void Enter()
        {
            _coin.ChangeModifier(_modifier);
        }

        public void Exit()
        {
            _coin.ChangeModifier(1);
        }
    }
}
