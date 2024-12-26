using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class EventSystemHolder
    {
        private List<IEvent> _events;

        private IEvent _currentEvent;

        public void Init(List<IEvent> eventsList)
        {
            _events = eventsList;
        }

        public void OnNewEvent()
        {
            int randomIndex = Random.Range(0, _events.Count);

            ChangeEvent(_events[randomIndex]);
        }

        private void ChangeEvent(IEvent newEvent)
        {
            _currentEvent?.Exit();
            _currentEvent = newEvent;
            _currentEvent.Enter();
        }
    }
}
