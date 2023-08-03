using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Controller : MonoBehaviour
    {
        private Jump jumpComponent;
        private bool hasJumpComponent = false;

        private Run runComponent;
        private bool hasRunComponent = false;

        void Start()
        {
            hasJumpComponent = TryGetComponent<Jump>(out jumpComponent);
            hasRunComponent = TryGetComponent<Run>(out runComponent);
        }

        void FixedUpdate()
        {
            if (hasRunComponent) runComponent.Action();
            if (hasJumpComponent) jumpComponent.Action();
        }
    }

}