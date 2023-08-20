using System.Collections;
using System.Collections.Generic;
using SO.Variables;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    public class Death : MonoBehaviour
    {
        [SerializeField] private BoolSO isDeath;
        [SerializeField] private FloatSO velocity;

        private Animator animator;

        private PlayerInput input;

        private Controller controller;

        private Jump jump;

        private Run run;

        private BoxCollider2D boxCollider2D;

        private Rigidbody2D rg;

        private void Awake()
        {
            TryGetComponent(out animator);
            TryGetComponent(out input);
            TryGetComponent(out controller);
            TryGetComponent(out jump);
            TryGetComponent(out run);
            TryGetComponent(out boxCollider2D);
            TryGetComponent(out rg);
        }

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
            if (velocity.Value > 0) velocity.SetNewValue(0);
            gameObject.SetActive(false);
        }

        private void OnVelocityZero()
        {
            StartCoroutine(DestroyPlayer());
        }

        private IEnumerator DestroyPlayer()
        {
            if (velocity.Value > 0) velocity.SetNewValue(0);
            run.enabled = false;
            rg.bodyType = RigidbodyType2D.Static;
            input.enabled = false;
            controller.enabled = false;
            jump.enabled = false;
            boxCollider2D.enabled = false;
            animator.SetBool("Has Die", true);
            yield return new WaitForSeconds(1);
            gameObject.SetActive(false);
            yield return null;
        }
    }
}