using SO;
using UnityEngine;
using Utils;

namespace Player
{
    public class Run : MonoBehaviour
    {
        [SerializeField] private SORun runner;
        [SerializeField] private SODistance distance;

        private Sensor groundSensor;

        void Awake()
        {
            bool hasGroundSensor = TryGetComponent(out groundSensor);

            if (!hasGroundSensor) groundSensor = gameObject.AddComponent<Sensor>();
        }

        private void Start()
        {
            runner.Init();
            distance.Init();
        }

        public void Action()
        {
            distance.AddDistance(runner.Velocity * Time.fixedDeltaTime);
            if (groundSensor.State)
            {
                runner.Acceleration = runner.MaxAcceleration * (1 - runner.VelocityRatio);
                runner.Velocity += runner.Acceleration * Time.fixedDeltaTime;
                CreateDust();
                if (runner.Velocity >= runner.MaxVelocity)
                    runner.Velocity = runner.MaxVelocity;
            }
            else
            {
                StopDust();
            }
        }

        public void DecreaseSpeed(float value)
        {
            runner.Velocity -= value;
        }
    }
}