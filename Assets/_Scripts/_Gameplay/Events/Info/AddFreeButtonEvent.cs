using Assets.Gameplay;

namespace Events
{
    public class AddFreeButtonEvent : IEvent
    {
        private EventData _data;
        private Bank _bank;
        private int _count;

        public AddFreeButtonEvent(EventData data, Bank bank, int count)
        {
            _data = data;
            _bank = bank;
            _count = count;
        }

        public EventData EventData => _data;

        public void Enter()
        {
            _bank.AddFreeButton(_count);
        }

        public void Exit()
        {
        }
    }
}
