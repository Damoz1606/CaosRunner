using System.Collections;
using System.Collections.Generic;
using PlayerActions;
using UnityEngine;
using Utils;

namespace Player
{
    public class Jump : MonoBehaviour
    {
        [SerializeField] private float jumpForce = 10f;

        private Sensor groundSensor;

        private JumpInputSystem jumpInputSystem;

        private Rigidbody2D rb;

        void Start()
        {
            bool hasInputSystem = TryGetComponent<JumpInputSystem>(out jumpInputSystem);
            bool hasRigidBody = TryGetComponent<Rigidbody2D>(out rb);
            bool hasGroundSensor = TryGetComponent<Sensor>(out groundSensor);

            if (!hasInputSystem)
                jumpInputSystem = gameObject.AddComponent<JumpInputSystem>();
            if (!hasRigidBody)
                rb = gameObject.GetComponent<Rigidbody2D>();
            if (!hasGroundSensor)
                groundSensor = gameObject.AddComponent<Sensor>();
        }


        public void Action()
        {
            if (groundSensor.State && jumpInputSystem.IsJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            if (!groundSensor.State && jumpInputSystem.IsJumping)
                jumpInputSystem.IsJumping = false;
        }
    }

}