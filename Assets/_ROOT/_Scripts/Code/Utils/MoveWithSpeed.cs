using SO;
using SO.Variables;
using UnityEngine;

namespace Utils
{
    public class MoveWithSpeed : MonoBehaviour
    {
        [SerializeField] private FloatSO runner;
        [SerializeField] private bool forward = false;

        private void Update()
        {
            Vector2 nextPosition = transform.position;
            nextPosition.x += (forward ? 1 : -1) * runner.Value * Time.deltaTime;

            transform.position = nextPosition;
        }
    }
}