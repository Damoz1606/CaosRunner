using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Controller : MonoBehaviour
    {
        private Jump jumpComponent;

        private Run runComponent;

        void Start()
        {
            TryGetComponent(out jumpComponent);
            TryGetComponent(out runComponent);
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