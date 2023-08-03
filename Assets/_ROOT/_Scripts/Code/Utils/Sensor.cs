using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class Sensor : MonoBehaviour
    {
        [SerializeField] private LayerMask collisonMask;
        [SerializeField] private float collisionDistance = 1f;
        [SerializeField] private bool state = false;

        public bool State { get => this.state; }

        private void FixedUpdate()
        {
            this.CheckProximity();
        }

        private void CheckProximity()
        {
            RaycastHit2D hit;
            Vector2 position = this.transform.position;
            Vector2 direction = Vector2.down;

            hit = Physics2D.Raycast(position, direction, this.collisionDistance, this.collisonMask);
            this.state = hit.collider != null;
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(this.transform.position, Vector2.down * this.collisionDistance, this.state ? Color.red : Color.green);
        }
    }
}
