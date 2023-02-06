﻿using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = SimpleInputAxis();

                if (axis == Vector2.zero) 
                    axis = UnityAxis();

                return axis;
            }
        }

        public override bool IsJumpButtonUp => 
            UnityEngine.Input.GetButtonUp(Jump) || SimpleInput.GetButtonUp(Jump);

        private static Vector2 UnityAxis() => 
            new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
    }
}