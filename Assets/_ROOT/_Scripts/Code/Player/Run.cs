using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Player
{
    public class Run : MonoBehaviour
    {
        [SerializeField] private float velocity = 0;
        [SerializeField] private float maxVelocity = 100;
        [SerializeField] private float acceleration = 10;
        [SerializeField] private float maxAcceleration = 10;

        [SerializeField] private float distance;

        public UnityAction<float> OnDistanceChange;

        private Rigidbody2D rb;

        private Sensor groundSensor;

        public float Velocity { get => velocity; set => velocity = value; }
        public float Acceleration { get => acceleration; set => acceleration = value; }
        public float MaxAcceleration { get => maxAcceleration; set => maxAcceleration = value; }
        public float MaxVelocity { get => maxVelocity; set => maxVelocity = value; }
        public float Distance { get => distance; set => distance = value; }

        void Start()
        {
            bool hasGroundSensor = TryGetComponent<Sensor>(out groundSensor);
            bool hasRigidBody = TryGetComponent<Rigidbody2D>(out rb);

            if (!hasGroundSensor) groundSensor = gameObject.AddComponent<Sensor>();
            if (!hasRigidBody) rb = gameObject.AddComponent<Rigidbody2D>();
        }

        public void Action()
        {
            distance += velocity * Time.fixedDeltaTime;
            OnDistanceChange?.Invoke(distance);
            if (groundSensor.State)
            {
                float velocityRatio = velocity / maxVelocity;
                acceleration = maxAcceleration * (1 - velocityRatio);
                velocity += acceleration * Time.fixedDeltaTime;
                if (velocity >= maxVelocity)
                    velocity = maxVelocity;
            }
        }
    }
}