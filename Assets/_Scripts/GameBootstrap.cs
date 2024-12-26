using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private GameController _gameController;

    private EventSystemHolder _eventSystemHolder;

    void Start()
    {
        _eventSystemHolder = new EventSystemHolder();
        _eventSystemHolder.Init(new EventsFabric().GetFullEventsList());

        _gameController.OnSliderEnded.OnSliderEndEvent += _eventSystemHolder.OnNewEvent;
    }

    private void OnDestroy()
    {
        _gameController.OnSliderEnded.OnSliderEndEvent -= _eventSystemHolder.OnNewEvent;        
    }
}
