using CodeBase.Services.Input;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, CurtainLoader curtain) => 
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain);
    }
}