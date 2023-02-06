namespace CodeBase.Infrastructure.States
{
    public interface IPayLoaded<T> : IExitableState
    {
        void Enter(T payload);
    }
}