using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public enum SensorType
    {
        RAYCAST,
        CIRCLE
    }

    public class Sensor : MonoBehaviour
    {
        [SerializeField] private LayerMask collisonMask;
        [SerializeField] private float collisionDistance = 1f;
        [SerializeField] private Color outColor = Color.green;
        [SerializeField] private Color inColor = Color.red;
        [SerializeField] private SensorType sensorType = SensorType.RAYCAST;

        #region Raycast
        [SerializeField] private Vector3 direction = Vector3.up;
        #endregion

        private bool state = false;

        public UnityAction<bool> OnChangeValue;

        protected bool lastState = false;

        public bool State { get => state; }

        protected virtual void FixedUpdate()
        {
            CheckProximity();
        }

        protected virtual void CheckProximity()
        {
            switch (sensorType)
            {
                case SensorType.RAYCAST:
                    RaycastHit2D hit;
                    Vector2 position = transform.position;

                    hit = Physics2D.Raycast(position, direction, collisionDistance, collisonMask);
                    state = hit.collider != null;
                    break;
                case SensorType.CIRCLE:
                    state = Physics2D.OverlapCircle(transform.position, collisionDistance, collisonMask);
                    break;
            }

            if (lastState != state)
            {
                lastState = state;
                OnChangeValue?.Invoke(state);
            }
        }

        protected virtual void OnDrawGizmos()
        {
            switch (sensorType)
            {
                case SensorType.RAYCAST:
                    Debug.DrawRay(transform.position, direction * collisionDistance, state ? inColor : outColor);
                    break;
                case SensorType.CIRCLE:
                    Gizmos.color = state ? inColor : outColor;
                    Gizmos.DrawWireSphere(transform.position, collisionDistance);
                    break;
            }
        }
    }
}
