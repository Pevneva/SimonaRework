using UnityEngine;

namespace CodeBase.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string Jump = "Jump";
        private const string Fire = "Fire1";

        public abstract Vector2 Axis { get; }

        public abstract bool IsJumpButtonUp { get;  }

        public bool IsAttackButtonUp() => UnityEngine.Input.GetButtonUp(Fire);
        
        protected static Vector2 SimpleInputAxis() 
            => new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}