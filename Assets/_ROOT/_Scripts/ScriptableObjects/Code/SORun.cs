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

        private UnityEvent<float> OnVelocityChange;
        private UnityEvent<float> OnVelocityRatioChange;

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

        public void Init()
        {
            velocity = 0;
            acceleration = 10;
        }

        public void StartListeningVelocityChange(UnityAction<float> action)
        {
            OnVelocityChange.AddListener(action);
        }

        public void StopListeningVelocityChange(UnityAction<float> action)
        {
            OnVelocityChange.RemoveListener(action);
        }

        public void StartListenVelocityRatioChange(UnityAction<float> action)
        {
            OnVelocityRatioChange.AddListener(action);
        }

        public void StopListenVelocityRatioChange(UnityAction<float> action)
        {
            OnVelocityRatioChange.RemoveListener(action);
        }
    }
}