using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Services.Factory;
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

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, CurtainLoader curtain, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string levelName)
        {
            _curtain.Show();
            _sceneLoader.Load(levelName, onLoaded: OnLoaded);
        }

        public void Exit() => _curtain.Hide();

        private void OnLoaded()
        {
            var hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
            _gameFactory.CreateHud();
            
            CameraFollow(hero);

            _stateMachine.Enter<TestState>();
        }

        private void CameraFollow(GameObject gameObject) => 
            Camera.main.GetComponent<CameraFollow>().Follow(gameObject);
    }
}