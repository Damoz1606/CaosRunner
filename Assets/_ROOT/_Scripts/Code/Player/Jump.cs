using System;
using System.Collections;
using System.Collections.Generic;
using PlayerActions;
using UnityEngine;
using Utils;

namespace Player
{
    public class Jump : MonoBehaviour
    {
        [SerializeField] private float minHeight = 10f;
        [SerializeField] private float maxHeight = 35f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float counterForce = 5f;

        private bool jumpWasLifted = false;

        private Run runner;

        private Sensor groundSensor;

        private JumpInputSystem jumpInputSystem;

        private Rigidbody2D rb;

        private void Awake()
        {
            bool hasRunner = TryGetComponent<Run>(out runner);
            bool hasInputSystem = TryGetComponent<JumpInputSystem>(out jumpInputSystem);
            bool hasRigidBody = TryGetComponent<Rigidbody2D>(out rb);
            bool hasGroundSensor = TryGetComponent<Sensor>(out groundSensor);

            if (!hasRunner)
                runner = gameObject.AddComponent<Run>();
            if (!hasInputSystem)
                jumpInputSystem = gameObject.AddComponent<JumpInputSystem>();
            if (!hasRigidBody)
                rb = gameObject.GetComponent<Rigidbody2D>();
            if (!hasGroundSensor)
                groundSensor = gameObject.AddComponent<Sensor>();
        }

        public void Action()
        {
            float minForce = CalculateJumpForce(Physics2D.gravity.magnitude * rb.gravityScale, minHeight, rb.mass);
            float maxForce = CalculateJumpForce(Physics2D.gravity.magnitude * rb.gravityScale, maxHeight, rb.mass);
            if (groundSensor.State && jumpInputSystem.IsJumping)
            {
                Debug.Log($"Max: {maxForce} - Min: {minForce}");
                float forceToBeAdded = Mathf.Clamp(jumpForce * rb.mass * runner.VelocityRatio, minForce, maxForce);
                rb.AddForce(Vector2.up * forceToBeAdded, ForceMode2D.Impulse);
                jumpInputSystem.IsJumping = false;
                jumpWasLifted = false;
            }
            if (!groundSensor.State && jumpInputSystem.IsHeld && !jumpWasLifted)
            {
                float forceToBeAdded = counterForce * runner.VelocityRatio;
                rb.AddForce(Vector2.up * forceToBeAdded, ForceMode2D.Impulse);
            }
            else if (!groundSensor.State && !jumpInputSystem.IsHeld)
            {
                jumpWasLifted = true;
            }
        }

        private float CalculateJumpForce(float gravity, float height, float mass)
        {
            float ep = mass * gravity * height;
            return Mathf.Sqrt(2 * ep * 1 / mass);
        }
    }

}