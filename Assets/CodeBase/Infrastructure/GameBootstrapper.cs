using CodeBase.Infrastructure.States;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private CurtainLoader _curtainPrefab;
        
        private Game _game;
        private IInputService _inputService;
        private GameStateMachine _stateMachine;

        private void Awake()
        {
            _game = new Game(this, _curtainPrefab);
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}
