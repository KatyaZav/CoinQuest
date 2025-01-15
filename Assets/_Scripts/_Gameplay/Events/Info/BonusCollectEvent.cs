using Assets.Gameplay;

namespace Events
{
    public class BonusCollectEvent : IEvent
    {
        private EventData _data;
        private float _modifier;
        private ItemGenerator _generator;

        public BonusCollectEvent(EventData data, float modifier, ItemGenerator coin)
        {
            _data = data;
            _modifier = modifier;
            _generator = coin;
        }

        public EventData EventData => _data;

        public void Enter()
        {
            _generator.ChangeCountModifier(_modifier);
        }

        public void Exit()
        {
            _generator.ChangeCountModifier(1);
        }
    }
}
