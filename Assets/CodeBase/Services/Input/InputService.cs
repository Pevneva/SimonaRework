using UnityEngine;

namespace CodeBase.Services.Input
{
    class InputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const string Jump = "Jump";
        private const string Fire = "Fire1";

        public Vector2 Axis => 
            new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));

        public bool IsJumpButtonUp => UnityEngine.Input.GetButtonUp(Jump);

        public bool IsAttackButtonUp() => UnityEngine.Input.GetButtonUp(Fire);
    }
}