using UnityEngine;
using YG;

namespace Events
{
    public class RewardBankEvent : IEvent
    {
        private EventData _data;
        private int _count;

        public RewardBankEvent(EventData data, int count)
        {
            _data = data;
            _count = count;
        }

        public EventData EventData => _data;

        public void Enter()
        {
            YandexGame.FullscreenShow();

            PlayerSaves.AddCoins(_count);
            PlayerSaves.PutCoinsToBank();
        }

        public void Exit()
        {
        }
    }
}