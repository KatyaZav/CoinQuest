using Assets.Gameplay;
using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Random = UnityEngine.Random;

public class ChangeRoomEvent : IEvent
{
    private ItemGenerator _generator;
    private EventData _data;

    public ChangeRoomEvent(ItemGenerator generatro, EventData data)
    {
        _generator = generatro;
        _data = data;
    }

    public EventData EventData => _data;

    public void Enter()
    {
        List<Room> rooms = Enum.GetValues(typeof(Room)).Cast<Room>().ToList();
        rooms.Remove(_generator.CurrentRoom);

        Room newRoom = rooms[Random.Range(0, rooms.Count)];

        _generator.ChangeRoom(newRoom);
    }

    public void Exit()
    {
    }

    public void OnPopupClosed()
    {
    }
}
