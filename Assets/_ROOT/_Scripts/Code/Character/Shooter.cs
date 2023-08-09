using System.Collections;
using System.Collections.Generic;
using Environment;
using Manager;
using Pool;
using SO.Variables;
using UnityEngine;
using Utils;

namespace Character
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private FloatSO runner;

        [SerializeField] private float bulletMoveSpeed;
        [SerializeField] private float bulletDistance;

        [SerializeField] private int burstCount = 5;
        [SerializeField] private int bulletsPerBurst = 10;
        [SerializeField] private float timeBetweenBurst = 1f;
        [SerializeField] private float restTimeAfterBurst = 1f;

        [SerializeField, Range(0, 359)] private int angleSpread = 0;

        [SerializeField] private float startingDistance = 0.1f;

        [SerializeField] private bool stagger = false;
        [SerializeField] private bool oscillate = false;

        private EnemySensor sensor;

        private bool isShooting = false;

        private void Awake()
        {
            TryGetComponent(out sensor);
        }

        private void Update()
        {
            if (sensor.State && !isShooting)
                StartCoroutine(ShootCoroutine());
        }

        private IEnumerator ShootCoroutine()
        {
            isShooting = true;
            float timeBetweenBullets = 0;

            TargetConeOfInfluence(out float startAngle, out float endAngle, out float currentAngle, out float angleStep);

            if (stagger) { timeBetweenBullets = timeBetweenBurst / bulletsPerBurst; }

            for (int i = 0; i < burstCount; i++)
            {
                if (stagger && oscillate)
                {
                    float flag = Mathf.Pow(-1, i + 2);
                    currentAngle = flag < 0 ? endAngle : startAngle;
                    angleStep *= flag;
                }

                for (int j = 0; j < bulletsPerBurst; j++)
                {
                    Vector2 position = FindBulletSpawnPosition(currentAngle);
                    Bullet newBullet = BulletPool.SharedInstance.GetPooledObject();
                    if (newBullet != null)
                    {
                        newBullet.transform.position = position;
                        newBullet.transform.up = (newBullet.transform.position - transform.position).normalized;
                        newBullet.UpdateMoveSpeed(bulletMoveSpeed + runner.Value);
                        newBullet.UpdateBulletRange(bulletDistance);

                        newBullet.OnActiveBullet();
                    }

                    currentAngle += angleStep;

                    if (stagger) yield return new WaitForSeconds(timeBetweenBullets);
                }

                yield return new WaitForSeconds(timeBetweenBurst);
                TargetConeOfInfluence(out startAngle, out endAngle, out currentAngle, out angleStep);
            }

            yield return new WaitForSeconds(restTimeAfterBurst);
            isShooting = false;
        }

        private void TargetConeOfInfluence(out float startAngle, out float endAngle, out float currentAngle, out float angleStep)
        {
            Vector3 targetDirection = (MasterManager.SharedInstance.GameManager.Player.transform.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            startAngle = targetAngle;
            endAngle = targetAngle;
            currentAngle = targetAngle;
            angleStep = 0;
            if (angleSpread != 0)
            {
                float bulletAmount = bulletsPerBurst - 1;
                angleStep = angleSpread / (bulletAmount == 0 ? 1 : bulletAmount);
                float halfAngleSpread = angleSpread / 2f;
                startAngle = targetAngle - halfAngleSpread;
                endAngle = targetAngle + halfAngleSpread;
                currentAngle = startAngle;
            }
        }

        private Vector2 FindBulletSpawnPosition(float currentAngle)
        {
            return new(
                transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad),
                transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad)
            );
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Vector3 targetDirection = new(-1, 0, 0);
            float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            float startAngle = targetAngle;
            float endAngle = targetAngle;

            if (angleSpread != 0)
            {
                float halfAngleSpread = angleSpread / 2f;
                startAngle = targetAngle - halfAngleSpread;
                endAngle = targetAngle + halfAngleSpread;
            }

            // Calculate cone base points
            Vector3 basePoint1Start = new(
                transform.position.x + startingDistance * Mathf.Cos(startAngle * Mathf.Deg2Rad),
                transform.position.y + startingDistance * Mathf.Sin(startAngle * Mathf.Deg2Rad)
                );
            Vector3 basePoint2Start = new(
                transform.position.x + startingDistance * Mathf.Cos(endAngle * Mathf.Deg2Rad),
                transform.position.y + startingDistance * Mathf.Sin(endAngle * Mathf.Deg2Rad)
                );

            Vector3 basePoint1End = new(
            transform.position.x + bulletDistance * Mathf.Cos(startAngle * Mathf.Deg2Rad),
            transform.position.y + bulletDistance * Mathf.Sin(startAngle * Mathf.Deg2Rad)
            );
            Vector3 basePoint2End = new(
                transform.position.x + bulletDistance * Mathf.Cos(endAngle * Mathf.Deg2Rad),
                transform.position.y + bulletDistance * Mathf.Sin(endAngle * Mathf.Deg2Rad)
                );

            // Draw cone base lines
            Gizmos.DrawLine(basePoint1Start, basePoint1End);
            Gizmos.DrawLine(basePoint2Start, basePoint2End);

            // Draw cone side lines
            Vector3 conebase = transform.position + targetDirection.normalized * startingDistance;
            Gizmos.DrawLine(conebase, basePoint1Start);
            Gizmos.DrawLine(conebase, basePoint2Start);
            Vector3 coneTip = transform.position + targetDirection.normalized * bulletDistance;
            Gizmos.DrawLine(coneTip, basePoint1End);
            Gizmos.DrawLine(coneTip, basePoint2End);
        }
    }

}