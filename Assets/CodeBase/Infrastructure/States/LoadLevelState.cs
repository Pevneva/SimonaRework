using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayLoaded<string>
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly CurtainLoader _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgress;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, CurtainLoader curtain, IGameFactory gameFactory, IPersistentProgressService persistentProgress)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _persistentProgress = persistentProgress;
        }

        public void Enter(string levelName)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(levelName, onLoaded: OnLoaded);
        }

        public void Exit() => _curtain.Hide();

        private void OnLoaded()
        {
            InitGameWorld();

            LoadProgress();

            _stateMachine.Enter<TestState>();
        }

        private void LoadProgress()
        {
            foreach (ILoadProgress loader in _gameFactory.ProgressLoaders) 
                loader.Load(_persistentProgress.Progress);
        }

        private void InitGameWorld()
        {
            var hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
            _gameFactory.CreateHud();
            CameraFollow(hero);
        }

        private void CameraFollow(GameObject gameObject) => 
            Camera.main.GetComponent<CameraFollow>().Follow(gameObject);
    }
}