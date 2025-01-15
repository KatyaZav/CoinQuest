using System;
using UnityEngine.UI;

namespace Assets.Gameplay.Ipnut
{
    public class PlayerInput
    {
        public event Action ItemDropedEvent, ItemCollectedEvent;

        private Button _yesButton, _noButton;

        public PlayerInput(Button yesButton, Button noButton)
        {
            _yesButton = yesButton;
            _noButton = noButton;
        }

        public void OnInit()
        {
            _yesButton.onClick.AddListener(OnYesButtonClick);
            _noButton.onClick.AddListener(OnNoButtonClick);
        }

        public void OnDispose()
        {
            _yesButton.onClick.RemoveListener(OnYesButtonClick);
            _noButton.onClick.RemoveListener(OnNoButtonClick);
        }

        private void OnNoButtonClick()
        {
            ItemDropedEvent?.Invoke();
        }

        private void OnYesButtonClick()
        {
            ItemCollectedEvent?.Invoke();
        }
    }
}
