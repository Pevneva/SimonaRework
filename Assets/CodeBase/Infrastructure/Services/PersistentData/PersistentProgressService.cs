using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentData
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}