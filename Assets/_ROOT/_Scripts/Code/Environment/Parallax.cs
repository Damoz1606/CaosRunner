using SO;
using UnityEngine;

namespace Environment
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private SORun runner;
        [SerializeField] private float depth = 1;

        void FixedUpdate()
        {
            float realVelocity = runner.Velocity / depth;
            Vector2 position = transform.position;
            position.x -= realVelocity * Time.fixedDeltaTime;

            if (position.x < -transform.localScale.x / 2)
            {
                position.x = 18;
            }

            transform.position = position;
        }
    }

}