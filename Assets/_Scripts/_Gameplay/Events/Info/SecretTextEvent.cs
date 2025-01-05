using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class SecretTextEvent : IEvent
    {
        private Coin _coin;
        private EventData _data;
        private int _probability;
        private string _text;

        public SecretTextEvent(Coin coin, EventData data, int probability, string text)
        {
            _coin = coin;
            _data = data;
            _probability = probability;
            _text = text;
        }

        public EventData EventData => _data;

        public void Enter()
        {
            _coin.ChangeExtraProbability(_probability);
            _coin.MakeTextSecret(_text);
        }

        public void Exit()
        {
            _coin.ChangeExtraProbability(_probability * -1);
            _coin.DisactivateTextSecret();
        }
    }
}
