using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SO;
using UnityEngine;

namespace Enviroment
{
    public class Ground : MonoBehaviour
    {
        [SerializeField] private SORun runner;

        [SerializeField] private Obstacle obstaclePrefab;

        [SerializeField] private float groundHeight;
        [SerializeField] private float groundRight;
        [SerializeField] private float screenRight;

        [SerializeField] private float minDistince = 0.5f;
        [SerializeField] private float maxDistince = 10;

        bool didGenerateFloor = false;

        private BoxCollider2D bCollider;

        public float GroundHeight { get => groundHeight; set => groundHeight = value; }

        private void Awake()
        {
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

            Ground newFloor = Instantiate(gameObject, spawnPosition, Quaternion.identity).GetComponent<Ground>();

            newFloor.GroundHeight = newFloor.transform.position.y - distanceY + (newFloor.transform.localScale.y / 2);

            int numberObstacles = Random.Range(0, 4);
            List<float> usedPositions = new List<float>();

            float minObstaclePositionX = newFloor.transform.position.x - (newFloor.transform.localScale.x / 2);
            float maxObstaclePositionX = newFloor.transform.position.x + (newFloor.transform.localScale.x / 2);

            float offsetNegativeX = -obstaclePrefab.transform.localScale.x / 2;
            float offsetPositiveX = obstaclePrefab.transform.localScale.x / 2;

            for (int i = 0; i < numberObstacles; i++)
            {
                float newPositionX = Random.Range(minObstaclePositionX, maxObstaclePositionX);
                Obstacle tObstacle = Instantiate(obstaclePrefab, new Vector3(newPositionX, newFloor.groundHeight), Quaternion.identity);
                tObstacle.transform.SetParent(newFloor.transform);
            }


        }
    }
}