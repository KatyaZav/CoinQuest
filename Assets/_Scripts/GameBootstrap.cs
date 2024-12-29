using UnityEngine;
using Events;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private UIEventPopup _popup;

    private EventSystemHolder _eventSystemHolder;

    void Start()
    {
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
    }
}
