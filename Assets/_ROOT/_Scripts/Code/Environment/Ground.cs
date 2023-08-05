using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enviroment
{
    public class Ground : MonoBehaviour
    {
        [SerializeField] private float groundHeight;
        [SerializeField] private float groundRight;
        [SerializeField] private float screenRight;

        [SerializeField] private float minDistince = 0.5f;
        [SerializeField] private float maxDistince = 10;

        private Player.Run runner;

        bool didGenerateFloor = false;

        private BoxCollider2D bCollider;

        private void Awake()
        {
            bool hasRunner = GameObject.Find("Player").TryGetComponent<Player.Run>(out runner);
            if (!hasRunner) throw new System.Exception("There is no runner in the scene");

            bCollider = gameObject.GetComponent<BoxCollider2D>();
            groundHeight = transform.position.y + (bCollider.size.y / 2);
            screenRight = Camera.main.transform.position.x * 2;
        }


        private void FixedUpdate()
        {
            Vector2 position = transform.position;
            position.x -= runner.Velocity * Time.fixedDeltaTime * 0.5f;

            groundRight = transform.position.x + transform.localScale.x / 2;

            if (groundRight < 0)
            {
                Destroy(gameObject);
                return;
            }

            if (!didGenerateFloor)
            {
                if (groundRight < screenRight)
                {
                    didGenerateFloor = true;
                    GenerateGround();
                }
            }

            transform.position = position;
        }

        private void GenerateGround()
        {
            float distanceX = Mathf.Clamp(maxDistince * runner.VelocityRatio, minDistince, maxDistince);

            float distanceY = (runner.VelocityRatio >= 0.5)
            ? Random.Range(-transform.localScale.y / 3, transform.localScale.y / 2) * runner.VelocityRatio
            : Random.Range(0, transform.position.y);


            Vector3 spawnPosition = new(transform.localScale.x / 2 + screenRight + distanceX, distanceY);

            Instantiate(gameObject, spawnPosition, Quaternion.identity);
        }
    }
}