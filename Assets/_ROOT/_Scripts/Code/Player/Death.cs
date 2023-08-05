using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class Death : MonoBehaviour
    {
        public UnityAction OnDeathByFalling;

        void Update()
        {
            if (transform.position.y < 0)
            {
                OnDeathByFalling?.Invoke();
            }
        }
    }
}