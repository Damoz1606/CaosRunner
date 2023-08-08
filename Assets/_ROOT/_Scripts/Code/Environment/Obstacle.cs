using Manager;
using SO.Variables;
using UnityEngine;
using UnityEngine.Events;

namespace Environment
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private float decreaseValue = 10f;

        [SerializeField] private FloatSO combo;
        [SerializeField] private FloatSO velocity;

        [SerializeField] private LayerMask playerMask;

        [SerializeField] private Utils.Sensor sensor;

        public UnityAction OnDeactive;

        private void Awake()
        {
            TryGetComponent(out sensor);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((playerMask.value & 1 << other.gameObject.layer) > 0)
            {
                velocity.SetNewValue(velocity.Value - decreaseValue);
                OnDeactiveObstacle();
            }
        }

        private void OnEnable()
        {
            sensor.OnChangeValue += OnNearestJump;
        }

        private void OnDisable()
        {
            sensor.OnChangeValue -= OnNearestJump;
        }

        private void OnNearestJump(bool value)
        {
            if (value) combo.ChangeValueBy(1);
        }

        public void OnActiveObstacle()
        {
            gameObject.SetActive(true);
        }

        public void OnDeactiveObstacle()
        {
            OnDeactive?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
