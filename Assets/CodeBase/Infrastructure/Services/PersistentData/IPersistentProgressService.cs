using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentData
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}