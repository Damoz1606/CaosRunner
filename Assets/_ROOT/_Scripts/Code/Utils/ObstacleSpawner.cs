using Environment;
using Pool;
using UnityEngine;

namespace Utils
{
    public class ObstacleSpawner : MonoBehaviour
    {
        private Obstacle currentObstacle;
        public void Init()
        {
            Obstacle aux = ObstaclesPool.SharedInstance.GetPooledObject();
            if (currentObstacle != null) currentObstacle = null;
            if (aux != null)
            {
                currentObstacle = aux;
                currentObstacle.transform.position = transform.position;
                currentObstacle.OnDeactive += OnForceDeactive;
                currentObstacle.OnActiveObstacle();
            }
        }

        private void OnForceDeactive()
        {
            currentObstacle.OnDeactive -= OnForceDeactive;
            currentObstacle = null;
        }

        public void Deactive()
        {
            if (currentObstacle == null) return;
            currentObstacle.OnDeactive -= OnForceDeactive;
            currentObstacle?.OnDeactiveObstacle();
            currentObstacle = null;
        }
    }
}