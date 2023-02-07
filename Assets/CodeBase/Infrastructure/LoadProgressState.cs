using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class LoadProgressState : IState
    {
        private const string FirstLevelScene = "Main";
        private readonly GameStateMachine _stateMachine;
        private IPersistentProgressService _persistentProgressService;
        private ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadLevelState,string>(_persistentProgressService.Progress.WorldData.PositionOnLevel.Level);
        }

        private void LoadProgressOrInitNew() => 
            _persistentProgressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress() => 
            new PlayerProgress(FirstLevelScene);

        public void Exit()
        {
        }
    }
}