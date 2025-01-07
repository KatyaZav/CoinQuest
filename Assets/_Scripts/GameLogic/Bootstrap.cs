using UnityEngine;
using Events;
using Assets.UI;
using UnityEngine.UI;
using Assets.Game.UI;

namespace Assets.Game
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private UIEventPopup _popup;

        [Header("Text")]
        [SerializeField] private Text _coinCountText;
        [SerializeField] private ReactiveUI<int> _coinText;

        [Header("Initable")]
        [SerializeField] private ItemPopupView _itemPopupView;

        private EventSystemHolder _eventSystemHolder;

        void Start()
        {
            _coinText = new ReactiveUI<int>(PlayerSaves.CoinsInPocket, _coinCountText);
            _coinText.OnInit();

            _itemPopupView.Init();

            _gameController.Init();

            _eventSystemHolder = new EventSystemHolder();
            _eventSystemHolder.Init(new EventsFabric().GetFullEventsList());

            _popup.Init(_eventSystemHolder);

            _gameController.OnSliderEnded.OnSliderEndEvent += _eventSystemHolder.OnNewEvent;
        }

        private void OnDisable()
        {
            _gameController.OnSliderEnded.OnSliderEndEvent -= _eventSystemHolder.OnNewEvent;

            _popup.Exit();

            _coinText.OnDisable();
            _itemPopupView.OnDisable();
        }
    }
}
