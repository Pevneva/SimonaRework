using CodeBase.CameraLogic;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayLoaded<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string levelName)
        {
            _sceneLoader.Load(levelName, onLoaded: OnLoaded);
        }

        private void OnLoaded()
        {
            var initialPoint = GameObject.FindWithTag("InitialPoint");
            
            GameObject hero = Instantiate("Hero/Hero", at: initialPoint.transform.position);
            Instantiate("Hud/Hud");
            
            CameraFollow(hero);

            _stateMachine.Enter<TestState>();
        }

        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
        
        private static GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public void Exit()
        {
        }
        
        private void CameraFollow(GameObject gameObject) => 
            Camera.main.GetComponent<CameraFollow>().Follow(gameObject);
    }
}