using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public interface ILoadProgress
    {
        void Load(PlayerProgress playerProgress);
    }
}