namespace Events
{
    public interface IRewardBankEvent
    {
        EventData EventData { get; }

        void Enter();
        void Exit();
    }
}