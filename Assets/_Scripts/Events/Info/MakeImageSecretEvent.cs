using UnityEngine;

namespace Events
{
    public class MakeImageSecretEvent : IEvent
    {
        private Coin _coin;
        private EventData _data;
        private Sprite _sprite;

        public MakeImageSecretEvent(Coin coin, EventData data, Sprite sprite)
        {
            _coin = coin;
            _data = data;
            _sprite = sprite;
        }

        public EventData EventData => _data;

        public void Enter()
        {
            _coin.MakeImageSecret(_sprite);
        }

        public void Exit()
        {
            _coin.DisactivateImageSecret();
        }
    }
}
