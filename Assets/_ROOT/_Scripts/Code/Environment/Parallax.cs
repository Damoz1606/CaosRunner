using SO.Variables;
using UnityEngine;

namespace Environment
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private FloatSO velocity;
        [SerializeField] private float depth = 1;

        void FixedUpdate()
        {
            float realVelocity = velocity.Value / depth;
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