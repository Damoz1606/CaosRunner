using System.Collections;
using System.Collections.Generic;
using Environment;
using UnityEngine;

namespace Character
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private float minDistanceToShoot = 1f;
        [SerializeField] private LayerMask playerMask;

        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletMoveSpeed;
        [SerializeField] private int bustCount;
        [SerializeField] private int bulletsPerBurst;
        [SerializeField, Range(0, 360)] private float angleSpread;
        [SerializeField] private float startingDistance = 0.1f;
        [SerializeField] private float timeBetweenBurst;
        [SerializeField] private float restTime = 1f;

        [SerializeField] private bool stagger = false;
        [SerializeField] private bool oscillate = false;

        private bool isShooting = false;

        private void Update()
        {
            bool playerInRange= Physics2D.OverlapCircle(transform.position, minDistanceToShoot, playerMask);

            if (playerInRange)
            {
                Attack();
            }
        }

        public void Attack()
        {
            if (!isShooting) StartCoroutine(ShootCoroutine());
        }

        private IEnumerator ShootCoroutine()
        {
            isShooting = true;

            float timeBetweenBullets = 0f;

            ConeOfInfluence(out float startAngle, out float endAngle, out float currentAngle, out float angleStep);

            if (stagger)
            {
                timeBetweenBullets = timeBetweenBurst / bulletsPerBurst;
            }

            for (int i = 0; i < bustCount; i++)
            {
                if (!oscillate)
                {
                    ConeOfInfluence(out startAngle, out endAngle, out currentAngle, out angleStep);
                }

                if (oscillate && i % 2 != 1)
                {
                    ConeOfInfluence(out startAngle, out endAngle, out currentAngle, out angleStep);
                }
                else if (oscillate)
                {
                    currentAngle = endAngle;
                    endAngle = startAngle;
                    startAngle = currentAngle;
                    angleStep *= -1;
                }
                for (int j = 0; j < bulletsPerBurst; j++)
                {
                    Vector2 spawnBulletPosition = FindBulletSpawnPosition(currentAngle);
                    GameObject newBullet = Instantiate(bulletPrefab, spawnBulletPosition, Quaternion.identity);

                    newBullet.transform.right = newBullet.transform.position - transform.position;

                    if (newBullet.TryGetComponent(out Bullet bullet))
                    {
                        bullet.UpdateMoveSpeed(bulletMoveSpeed);
                    }

                    currentAngle += angleStep;

                    if (stagger) { yield return new WaitForSeconds(timeBetweenBullets); }
                }
                currentAngle = startAngle;

                if (!stagger)
                {
                    yield return new WaitForSeconds(timeBetweenBurst);
                }
            }


            yield return new WaitForSeconds(restTime);

            isShooting = false;
        }

        private void ConeOfInfluence(out float startAngle, out float endAngle, out float currentAngle, out float angleStep)
        {
            float targetAngle = Mathf.Atan2(6, 1) * Mathf.Rad2Deg;
            startAngle = targetAngle;
            endAngle = targetAngle;
            currentAngle = targetAngle;
            angleStep = 0;
            if (angleSpread != 0)
            {
                angleStep = angleSpread / (bulletsPerBurst - 1);
                float halfAngleSpread = angleSpread / 2;
                startAngle = targetAngle - halfAngleSpread;
                endAngle = targetAngle + halfAngleSpread;
                currentAngle = startAngle;
            }
        }

        private Vector2 FindBulletSpawnPosition(float currentAngle)
        {
            float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle);
            float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle);

            Vector2 position = new(x, y);

            return position;
        }

        private void OnDrawGizmos()
        {
            /* Gizmos.color = Color.green;
            float halfAngleRad = angleSpread / 2f * Mathf.Deg2Rad;
            Vector3 forward = transform.right;

            // Calculate cone base points
            Vector3 basePoint1 = transform.position + Quaternion.Euler(0, 0, -halfAngleRad) * forward * startingDistance;
            Vector3 basePoint2 = transform.position + Quaternion.Euler(0, 0, halfAngleRad) * forward * startingDistance;

            // Draw cone base lines
            Gizmos.DrawLine(transform.position, basePoint1);
            Gizmos.DrawLine(transform.position, basePoint2);

            // Draw cone side lines
            Vector3 coneTip = transform.position + forward * minDistanceToShoot;
            Gizmos.DrawLine(coneTip, basePoint1);
            Gizmos.DrawLine(coneTip, basePoint2); */
        }
    }

}