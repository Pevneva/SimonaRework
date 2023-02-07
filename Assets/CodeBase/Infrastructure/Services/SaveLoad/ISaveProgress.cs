using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public interface ISaveProgress : ILoadProgress
    {
        void Save(PlayerProgress playerProgress);
    }
}