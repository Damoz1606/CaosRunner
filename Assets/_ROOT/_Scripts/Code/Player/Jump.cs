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
        private bool hasAnimator = false;

        private Vector2 lastPosition = Vector2.zero;

        private Utils.GroundSensor groundSensor;

        private Rigidbody2D rb;

        private Animator animator;

        private void Awake()
        {
            bool hasRigidBody = TryGetComponent(out rb);
            bool hasGroundSensor = TryGetComponent(out groundSensor);
            hasAnimator = TryGetComponent(out animator);

            if (!hasRigidBody)
                rb = gameObject.GetComponent<Rigidbody2D>();
            if (!hasGroundSensor)
                groundSensor = gameObject.AddComponent<Utils.GroundSensor>();

            if (!hasAnimator)
                throw new System.Exception("There is no animator in the character");
        }

        private void Start()
        {
            lastPosition = transform.position;
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
            if (hasAnimator) OnAnimation();
        }

        private void OnAnimation()
        {
            if (lastPosition.y != transform.position.y)
            {
                if (lastPosition.y < transform.position.y)
                {
                    animator.SetInteger("Y", 1);
                }
                else
                {
                    animator.SetInteger("Y", -1);
                }
                lastPosition = transform.position;
            }
            else
            {
                animator.SetInteger("Y", 0);
            }
            if (groundSensor.State)
            {
                animator.SetBool("On Ground", groundSensor.State);
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
            if (hasAnimator)
            {
                animator.SetInteger("Y", 1);
                animator.SetBool("On Ground", false);
            }
        }

        private void EndJump()
        {
            timer = 0;
            isJumping = false;
        }
    }

}