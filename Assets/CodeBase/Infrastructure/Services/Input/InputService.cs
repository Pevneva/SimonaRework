using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string Jump = "Jump";
        protected const string Fire = "Fire2";

        public abstract Vector2 Axis { get; }

        public abstract bool IsJumpButtonUp { get;  }
        public abstract bool IsAttackButtonUp { get;  }

        protected static Vector2 SimpleInputAxis() 
            => new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}