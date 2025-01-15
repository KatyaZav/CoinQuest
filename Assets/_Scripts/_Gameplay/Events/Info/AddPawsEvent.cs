namespace Events
{
    public class AddPawsEvent : IEvent
    {
        private EventData _data;
        private int _pawCoins;

        public AddPawsEvent(EventData data, int pawCoins)
        {
            _data = data;
            _pawCoins = pawCoins;
        }

        public EventData EventData => _data;

        public void Enter()
        {
            PlayerSaves.AddCoins(_pawCoins);
        }

        public void Exit()
        {
        }

        public void OnPopupClosed()
        {

        }
    }
}
