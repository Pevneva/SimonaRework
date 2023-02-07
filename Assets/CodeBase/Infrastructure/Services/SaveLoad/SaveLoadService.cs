using CodeBase.Data;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService persistentProgress, IGameFactory gameFactory)
        {
            _persistentProgress = persistentProgress;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (var savedData in _gameFactory.ProgressSavers) 
                savedData.Save(_persistentProgress.Progress);

            PlayerPrefs.SetString(ProgressKey, _persistentProgress.Progress.ToJson());
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(ProgressKey)?.DoSerialized<PlayerProgress>();
    }
}