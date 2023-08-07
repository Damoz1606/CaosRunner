using SO.Variables;
using UnityEngine;
using UnityEngine.Events;

namespace Environment
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private LayerMask playerMask;
        [SerializeField] private FloatSO velocity;
        [SerializeField] private float decreaseValue = 10f;

        public UnityAction OnDeactive;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((playerMask.value & 1 << other.gameObject.layer) > 0)
            {
                velocity.SetNewValue(velocity.Value - decreaseValue);
                OnDeactiveObstacle();
            }
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
