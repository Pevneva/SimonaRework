using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();
        public override bool IsJumpButtonUp => SimpleInput.GetButtonDown(Jump);
        public override bool IsAttackButtonUp => SimpleInput.GetButtonDown(Fire);
    }
}