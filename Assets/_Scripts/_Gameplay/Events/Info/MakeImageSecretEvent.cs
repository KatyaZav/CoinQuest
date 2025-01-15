using Assets.Gameplay;
using UnityEngine;

namespace Events
{
    public class MakeImageSecretEvent : IEvent
    {
        private ItemGenerator _coin;
        private EventData _data;
        private Sprite _sprite;

        public MakeImageSecretEvent(ItemGenerator coin, EventData data, Sprite sprite)
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

        public void OnPopupClosed()
        {

        }
    }
}
