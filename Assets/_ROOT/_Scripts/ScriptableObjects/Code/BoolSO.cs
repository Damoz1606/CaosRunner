using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SO.Variables
{
    [CreateAssetMenu(fileName = "Bool", menuName = "CaosRunner/Variables/BoolSO", order = 0)]
    public class BoolSO : ScriptableObject
    {
        private bool value = false;
        private bool initialValue = false;

        private UnityEvent<bool> OnValueChange;

        public bool Value { get => value; }

        [ContextMenu("Init")]
        public void Init()
        {
            value = initialValue;
        }

        public void ToggleValue()
        {
            value = !value;
            OnValueChange?.Invoke(value);
        }

        public void SetNewValue(bool newValue)
        {
            value = newValue;
            OnValueChange?.Invoke(value);
        }

        public void StartListeningOnChange(UnityAction<bool> action)
        {
            OnValueChange.AddListener(action);
        }

        public void StopListeningOnChange(UnityAction<bool> action)
        {
            OnValueChange.RemoveListener(action);
        }
    }
}