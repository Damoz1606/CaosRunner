using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerActions
{
    public class JumpInputSystem : MonoBehaviour
    {
        private bool isJumping;

        public bool IsJumping { get => isJumping; set => isJumping = value; }

        public void OnJump(InputValue value) => JumpInput(value.isPressed);

        private void JumpInput(bool newJumpState)
        {
            isJumping = newJumpState;
        }
    }

}