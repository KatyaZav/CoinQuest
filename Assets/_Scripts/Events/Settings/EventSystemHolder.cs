using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Events
{
    public class EventSystemHolder
    {
        public Action<EventData> ChangedEvent;

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

            ChangedEvent?.Invoke(_events[randomIndex].EventData);
        }

        public void OnDisable()
        {
            _currentEvent?.Exit();
        }

        private void ChangeEvent(IEvent newEvent)
        {
            _currentEvent?.Exit();
            _currentEvent = newEvent;
            _currentEvent.Enter();
        }
    }
}
