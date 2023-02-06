using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayLoaded<string>
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly CurtainLoader _curtain;
        private IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, CurtainLoader curtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
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