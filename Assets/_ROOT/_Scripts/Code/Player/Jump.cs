using System;
using SO;
using SO.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Jump : MonoBehaviour
    {
        [SerializeField] private FloatSO velocity;

        [SerializeField] private float minJumpTime = 0.10f;
        [SerializeField] private float maxJumpTime = 0.35f;
        [SerializeField] private float force = 10f;

        private float timer = 0;

        private bool isJumping = false;

        private Utils.Sensor groundSensor;

        private Rigidbody2D rb;

        private void Awake()
        {
            bool hasRigidBody = TryGetComponent(out rb);
            bool hasGroundSensor = TryGetComponent(out groundSensor);

            if (!hasRigidBody)
                rb = gameObject.GetComponent<Rigidbody2D>();
            if (!hasGroundSensor)
                groundSensor = gameObject.AddComponent<Utils.Sensor>();
        }

        public void ActionUpdate()
        {
            if (isJumping)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                    EndJump();
                else
                    StartJump(false);
            }
        }

        private void OnJump(InputValue value)
        {
            if (groundSensor.State && value.isPressed) StartJump();
            else if (isJumping) EndJump();
        }

        private void StartJump(bool started = true)
        {
            rb.velocity = Vector2.up * force;
            if (started) timer = Mathf.Clamp(maxJumpTime * velocity.ValueRatio, minJumpTime, maxJumpTime);
            if (started) isJumping = true;
        }

        private void EndJump()
        {
            timer = 0;
            isJumping = false;
        }
    }

}