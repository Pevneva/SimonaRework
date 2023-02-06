using CodeBase.CameraLogic;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayLoaded<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string HeroPath = "Hero/Hero";
        private const string HudPath = "Hud/Hud";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly CurtainLoader _curtain;

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
            var initialPoint = GameObject.FindWithTag(InitialPointTag);
            
            GameObject hero = Instantiate(HeroPath, at: initialPoint.transform.position);
            Instantiate(HudPath);
            
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

        private void CameraFollow(GameObject gameObject) => 
            Camera.main.GetComponent<CameraFollow>().Follow(gameObject);
    }
}