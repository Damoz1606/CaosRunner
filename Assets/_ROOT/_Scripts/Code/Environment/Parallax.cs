using SO;
using UnityEngine;

namespace Enviroment
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

            if (position.x < -12)
            {
                position.x = 22;
            }

            transform.position = position;
        }
    }

}