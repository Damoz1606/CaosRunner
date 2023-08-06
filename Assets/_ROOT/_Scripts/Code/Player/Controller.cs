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
            hasJumpComponent = TryGetComponent(out jumpComponent);
            hasRunComponent = TryGetComponent(out runComponent);
        }

        private void Update()
        {
            jumpComponent.ActionUpdate();
        }

        void FixedUpdate()
        {
            runComponent.Action();
        }
    }

}