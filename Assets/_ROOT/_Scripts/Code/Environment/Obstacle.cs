using UnityEngine;

namespace Enviroment
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private LayerMask playerMask;
        [SerializeField] private float decreaseValue = 10f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((playerMask.value & 1 << other.gameObject.layer) > 0)
            {
                Player.Run runner = other.GetComponent<Player.Run>();
                if (runner != default)
                {
                    runner.DecreaseSpeed(decreaseValue);
                }
            }
        }
    }
}
