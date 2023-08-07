using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pool;
using SO;
using SO.Variables;
using UnityEngine;

namespace Environment
{
    public class Ground : MonoBehaviour
    {
        [SerializeField] private FloatSO velocity;

        [SerializeField] private float maxDistance = 5;
        [SerializeField] private float minDistance = 0;

        [SerializeField] private int maxObstacles = 3;

        private float screenRight;
        private float groundRight;

        private bool didGroundBeenGenerated = false;

        private BoxCollider2D boxCollider2D;

        [SerializeField] private List<Utils.ObstacleSpawner> obstacleSpawners;

        private void Awake()
        {
            screenRight = Camera.main.transform.position.x * 2;
            TryGetComponent(out boxCollider2D);
            obstacleSpawners = new(GetComponentsInChildren<Utils.ObstacleSpawner>());
        }

        public void OnActiveGround()
        {
            didGroundBeenGenerated = false;
            gameObject.SetActive(true);
            ActiveObstacles();
        }

        public void OnDeactiveGround()
        {
            DeactiveObstacles();
            gameObject.SetActive(false);
        }

        private void FixedUpdate()
        {
            groundRight = transform.position.x + boxCollider2D.size.x / 2;

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
        }

        private void GenerateGround()
        {
            float distanceX = (velocity.ValueRatio >= 0.25)
            ? Random.Range(minDistance, maxDistance)
            : minDistance - 0.1f;

            distanceX = (distanceX < 1) ? Mathf.Ceil(distanceX) : distanceX;

            float distanceY = (velocity.ValueRatio >= 0.25)
            ? Random.Range(-boxCollider2D.size.y / 2, boxCollider2D.size.y / 2) * velocity.ValueRatio
            : transform.position.y;

            Ground newGround = GroundPool.SharedInstance.GetPooledObject();
            if (newGround == null) return;
            Vector3 spawnPosition = new(newGround.boxCollider2D.size.x / 2 + groundRight + distanceX, distanceY);
            newGround.transform.position = spawnPosition;
            newGround.OnActiveGround();
        }

        private void ActiveObstacles()
        {
            if (obstacleSpawners.Count <= 0) return;
            List<Utils.ObstacleSpawner> auxList = new();
            auxList.AddRange(obstacleSpawners);
            for (int i = 0; i < maxObstacles; i++)
            {
                int itemIndex = Random.Range(0, auxList.Count);
                Utils.ObstacleSpawner obj = auxList[itemIndex];
                obj.Init();
                auxList.RemoveAt(itemIndex);
            }
        }

        private void DeactiveObstacles()
        {
            foreach (Utils.ObstacleSpawner obj in obstacleSpawners)
            {
                obj.Deactive();
            }
        }
    }
}