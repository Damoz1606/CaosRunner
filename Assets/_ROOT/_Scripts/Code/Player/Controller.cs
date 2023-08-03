using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Controller : MonoBehaviour
    {
        private Jump jumpComponent;

        private bool hasJumpComponent = false;

        void Start()
        {
            hasJumpComponent = TryGetComponent<Jump>(out jumpComponent);
        }

        void FixedUpdate()
        {
            if (hasJumpComponent) jumpComponent.Action();
        }
    }

}