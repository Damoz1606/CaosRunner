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

        private Sensor groundSensor;

        void Awake()
        {
            bool hasGroundSensor = TryGetComponent(out groundSensor);

            if (!hasGroundSensor) groundSensor = gameObject.AddComponent<Sensor>();
        }

        private void Start()
        {
            velocity.Init();
            acceleration.Init();
            distance.Init();
        }

        public void Action()
        {
            if (!isAlive) return;
            distance.ChangeValueBy(velocity.Value * Time.fixedDeltaTime);
            if (groundSensor.State)
            {
                acceleration.SetNewValue(acceleration.MaxValue * (1 - velocity.ValueRatio));
                velocity.ChangeValueBy(acceleration.Value * Time.fixedDeltaTime);
            }
        }
    }
}