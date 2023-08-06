using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SO
{
    [CreateAssetMenu(fileName = "Run", menuName = "CaosRunner/Player/Variables/Run", order = 0)]
    public class SORun : ScriptableObject
    {
        [SerializeField] private float velocity = 0;
        [SerializeField] private float maxVelocity = 100;

        [SerializeField] private float acceleration = 10;
        [SerializeField] private float maxAcceleration = 10;

        public UnityEvent<float> OnVelocityChange;
        public UnityEvent<float> OnVelocityRatioChange;

        public float Velocity
        {
            get => velocity; set
            {
                velocity = value;
                OnVelocityChange.Invoke(velocity);
                OnVelocityRatioChange.Invoke(VelocityRatio);
            }
        }
        public float MaxVelocity { get => maxVelocity; }
        public float VelocityRatio { get => Mathf.Clamp(velocity / maxVelocity, 0, 1); }
        public float Acceleration { get => acceleration; set => acceleration = value; }
        public float MaxAcceleration { get => maxAcceleration; }

        private void Reset()
        {
            velocity = 0;
            maxVelocity = 100;
            acceleration = 10;
            maxAcceleration = 10;
        }
    }
}