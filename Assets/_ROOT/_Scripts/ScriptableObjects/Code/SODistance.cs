using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SO
{
    [CreateAssetMenu(fileName = "Distance", menuName = "CaosRunner/Player/Variables/Distance", order = 0)]
    public class SODistance : ScriptableObject
    {

        [SerializeField] private float distance;

        private UnityEvent<float> OnDistanceChange;

        public void AddDistance(float newDistance)
        {
            distance += newDistance;
            OnDistanceChange?.Invoke(distance);
        }

        public void Init()
        {
            distance = 0;
        }

        public void StartListeningDistanceChange(UnityAction<float> action)
        {
            OnDistanceChange.AddListener(action);
        }
        public void StopListeningDistanceChange(UnityAction<float> action)
        {
            OnDistanceChange.RemoveListener(action);
        }
    }
}