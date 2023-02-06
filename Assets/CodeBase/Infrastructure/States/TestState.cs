using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class TestState : IState
    {
        public void Enter()
        {
            Debug.Log(" Hello ! We are in Test State !");
        }

        public void Exit()
        {
        }
    }
}