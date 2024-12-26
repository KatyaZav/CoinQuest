namespace Events
{
    public interface IEvent
    {
        public EventData EventData {get;}
        public void Enter();
        public void Exit();
        //public void Update(float deltaTime);
    }
}
