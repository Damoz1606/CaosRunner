using SO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Jump : MonoBehaviour
    {
        [SerializeField] private SORun runner;
        [SerializeField] private SOJump jumper;

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

        private void Update()
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

        public void OnJump(InputValue value)
        {
            if (groundSensor.State && value.isPressed) StartJump();
            else if (isJumping) EndJump();
        }

        public void StartJump(bool started = true)
        {
            rb.velocity = Vector2.up * jumper.Force;
            if (started) timer = Mathf.Clamp(jumper.MaxJumpTime * runner.VelocityRatio, jumper.MinJumpTime, jumper.MaxJumpTime);
            if (started) isJumping = true;
        }

        public void EndJump()
        {
            timer = 0;
            isJumping = false;
        }
    }

}