using System.Collections;
using System.Collections.Generic;
using SO.Variables;
using UnityEngine;

namespace Environment
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 22f;
        [SerializeField] private float bulletRange = 10f;
        [SerializeField] private float decreaseDamage = 10f;

        [SerializeField] private FloatSO velocity;

        [SerializeField] private LayerMask playerMask;

        private float currentDistance;

        public void OnActiveBullet()
        {
            gameObject.SetActive(true);
        }

        public void OnDeactiveBullet()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            MoveBullet();
            RangeBullet();
        }

        private void MoveBullet()
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        private void RangeBullet()
        {
            currentDistance += moveSpeed * Time.deltaTime;
            if (currentDistance >= bulletRange)
            {
                OnDeactiveBullet();
            }
        }

        public void UpdateBulletRange(float newBulletRange)
        {
            bulletRange = newBulletRange;
        }

        public void UpdateMoveSpeed(float newMoveSpeed)
        {
            moveSpeed = newMoveSpeed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((playerMask.value & 1 << other.gameObject.layer) > 0)
            {
                velocity.SetNewValue(velocity.Value - decreaseDamage);
                OnDeactiveBullet();
            }
        }

    }
}
