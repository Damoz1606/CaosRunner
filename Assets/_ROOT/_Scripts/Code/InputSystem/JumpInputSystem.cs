using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace PlayerActions
{
    public class JumpInputSystem : MonoBehaviour
    {
        [SerializeField] private float maxHeldTime = 1;
        [SerializeField] private float startedAt = 0;
        [SerializeField] private bool isHeld;
        [SerializeField] private bool isJumping = false;

        public bool IsHeld { get => isHeld; }
        public bool IsJumping { get => isJumping; set => isJumping = value; }

        public UnityAction<bool> OnHeldChange;

        private void Update()
        {
            if (startedAt > 0 && isHeld)
            {
                float currentTime = Math.Abs(Time.time - startedAt);
                if (currentTime >= maxHeldTime)
                {
                    isHeld = false;
                    startedAt = 0;
                    OnHeldChange?.Invoke(isHeld);
                }
            }
        }

        public void OnJump(InputValue value) => JumpInput(value.isPressed);

        private void JumpInput(bool newJumpState)
        {
            isHeld = newJumpState;
            isJumping = newJumpState;
            if (newJumpState)
            {
                startedAt = Time.time;
            }
            else
            {
                startedAt = 0;
            }
        }
    }

}