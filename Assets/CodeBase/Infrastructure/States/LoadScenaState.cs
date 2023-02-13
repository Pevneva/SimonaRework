using CodeBase.CameraLogic;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Logic;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadScenaState : IPayLoaded<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string EnemySpawnerTag = "EnemySpawner";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly CurtainLoader _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgress;

        public LoadScenaState(GameStateMachine stateMachine, SceneLoader sceneLoader, CurtainLoader curtain, IGameFactory gameFactory, IPersistentProgressService persistentProgress)
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
            InitSpawners();
            
            GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
            CameraFollow(hero);
            
            GameObject hud = _gameFactory.CreateHud();
            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<IHealth>());
        }

        private void InitSpawners()
        {
            foreach (GameObject spawnerObject in GameObject.FindGameObjectsWithTag(EnemySpawnerTag))
            {
                var spawner =  spawnerObject.GetComponent<EnemySpawner>();
                _gameFactory.RegisterSaveLoadItems(spawner);
            }
        }

        private void CameraFollow(GameObject gameObject) => 
            Camera.main.GetComponent<CameraFollow>().Follow(gameObject);
    }
}