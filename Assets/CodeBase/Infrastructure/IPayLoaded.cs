namespace CodeBase.Infrastructure
{
    public interface IPayLoaded<T> : IExitableState
    {
        void Enter(T payload);
    }
}