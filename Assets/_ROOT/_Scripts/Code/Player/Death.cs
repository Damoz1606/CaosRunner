using System.Collections;
using System.Collections.Generic;
using SO.Variables;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class Death : MonoBehaviour
    {
        [SerializeField] private BoolSO isDeath;
        [SerializeField] private FloatSO velocity;

        private void OnEnable()
        {
            velocity.StartListeningOnValueIsMin(OnVelocityZero);
            isDeath.StartListeningOnChange(OnFalled);
        }

        private void OnDisable()
        {
            velocity.StopListeningOnValueIsMin(OnVelocityZero);
            isDeath.StopListeningOnChange(OnFalled);
        }

        private void Update()
        {
            if (transform.position.y < 0)
            {
                velocity.SetNewValue(0);
                isDeath.SetNewValue(true);
            }
        }

        private void OnFalled(bool flag)
        {
            DestroyPlayer();
        }

        private void OnVelocityZero()
        {
            DestroyPlayer();
        }

        private void DestroyPlayer()
        {
            if (velocity.Value > 0) velocity.SetNewValue(0);
            gameObject.SetActive(false);
        }
    }
}