using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pool;
using SO;
using UnityEngine;

namespace Environment
{
    public class Ground : MonoBehaviour
    {
        [SerializeField] private SORun runner;

        [SerializeField] private float groundRight;
        [SerializeField] private float screenRight;

        [SerializeField] private float maxDistance = 5;
        [SerializeField] private float minDistance = 0;

        private bool didGroundBeenGenerated = false;

        private void Awake()
        {
            screenRight = Camera.main.transform.position.x * 2;
        }

        public void OnActiveGround()
        {
            didGroundBeenGenerated = false;
            gameObject.SetActive(true);
        }

        public void OnDeactiveGround()
        {
            gameObject.SetActive(false);
        }

        private void FixedUpdate()
        {
            Vector2 position = transform.position;
            position.x -= runner.Velocity * Time.fixedDeltaTime * 0.2f;

            groundRight = transform.position.x + transform.localScale.x / 2;

            if (groundRight < 0)
            {
                OnDeactiveGround();
                return;
            }

            if (!didGroundBeenGenerated)
            {
                if (groundRight < screenRight)
                {
                    didGroundBeenGenerated = true;
                    GenerateGround();
                }
            }

            transform.position = position;
        }

        private void GenerateGround()
        {
            float distanceX = (runner.VelocityRatio >= 0.25)
            ? Random.Range(minDistance, maxDistance)
            : minDistance - 0.1f;

            distanceX = (distanceX < 1) ? Mathf.Ceil(distanceX) : distanceX;

            float distanceY = (runner.VelocityRatio >= 0.25)
            ? Random.Range(-transform.localScale.y / 2, transform.localScale.y / 2) * runner.VelocityRatio
            : transform.position.y;



            Ground newGround = GroundPool.SharedInstance.GetPooledObject();
            Vector3 spawnPosition = new(newGround.transform.localScale.x / 2 + groundRight + distanceX, distanceY);
            newGround.transform.position = spawnPosition;
            newGround.OnActiveGround();
        }
    }
}