using SO.Variables;
using UnityEngine;
using Utils;

namespace Player
{
    public class Run : MonoBehaviour
    {
        [SerializeField] private FloatSO velocity;
        [SerializeField] private FloatSO acceleration;

        [SerializeField] private FloatSO distance;

        private bool isAlive = true;
        private bool hasAnimator = false;

        private Utils.GroundSensor groundSensor;

        private Animator animator;

        void Awake()
        {
            bool hasGroundSensor = TryGetComponent(out groundSensor);

            hasAnimator = TryGetComponent(out animator);

            if (!hasGroundSensor) groundSensor = gameObject.AddComponent<Utils.GroundSensor>();
        }

        private void Start()
        {
            velocity.Init();
            acceleration.Init();
            distance.Init();
        }

        public void ActionUpdate()
        {
            if (hasAnimator) OnAnimation();
        }

        public void ActionFixedUpdate()
        {
            if (!isAlive) return;
            distance.ChangeValueBy(velocity.Value * Time.fixedDeltaTime);
            if (groundSensor.State)
            {
                acceleration.SetNewValue(acceleration.MaxValue * (1 - velocity.ValueRatio));
                velocity.ChangeValueBy(acceleration.Value * Time.fixedDeltaTime);
            }
        }

        private void OnAnimation()
        {
            animator.SetFloat("Speed Multiplier", 1 + velocity.ValueRatio);
        }
    }
}