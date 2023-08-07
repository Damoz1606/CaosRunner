using UnityEngine;
using UnityEngine.Events;

namespace SO.Variables
{
    [CreateAssetMenu(fileName = "Float", menuName = "CaosRunner/Variables/FloatSO", order = 0)]
    public class FloatSO : ScriptableObject
    {
        [SerializeField] private float value = 0;
        [SerializeField] private float initialValue = 0;
        [SerializeField] private bool allowMaxAndMinValue = true;
        [SerializeField] private float minValue = 0;
        [SerializeField] private float maxValue = 10;
        private bool start = true;

        private UnityEvent OnValueIsMin;
        private UnityEvent OnValueIsMax;
        private UnityEvent<float> OnValueChange;
        private UnityEvent<float> OnValueRatioChange;

        public float ValueRatio { get => Mathf.Clamp(value / maxValue, 0, 1); }
        public float Value { get => value; }
        public float MinValue { get => minValue; }
        public float MaxValue { get => maxValue; }

        [ContextMenu("Init")]
        public void Init()
        {
            value = initialValue;
            start = true;
            InvokeOnChange();
        }

        public void ChangeValueBy(float changeBy)
        {
            float newValue = value + changeBy;
            value = !allowMaxAndMinValue ? newValue : Mathf.Clamp(value + changeBy, minValue, maxValue);
            InvokeOnChange();
        }

        public void SetNewValue(float newValue)
        {
            value = Mathf.Clamp(newValue, minValue, maxValue);
            InvokeOnChange();
        }

        private void InvokeOnChange()
        {
            if (!start && allowMaxAndMinValue && value <= minValue) OnValueIsMin?.Invoke();
            if (!start && allowMaxAndMinValue && value >= maxValue) OnValueIsMax?.Invoke();
            OnValueChange?.Invoke(value);
            OnValueRatioChange?.Invoke(ValueRatio);
            start = false;
        }

        public void StartListeningOnValueIsMin(UnityAction action)
        {
            OnValueIsMin.AddListener(action);
        }
        public void StopListeningOnValueIsMin(UnityAction action)
        {
            OnValueIsMin.RemoveListener(action);
        }
        public void StartListeningOnValueIsMax(UnityAction action)
        {
            OnValueIsMax.AddListener(action);
        }
        public void StopListeningOnValueIsMax(UnityAction action)
        {
            OnValueIsMax.RemoveListener(action);
        }
        public void StartListeningOnValueChange(UnityAction<float> action)
        {
            OnValueChange.AddListener(action);
        }
        public void StopListeningOnValueChange(UnityAction<float> action)
        {
            OnValueChange.RemoveListener(action);
        }
        public void StartListeningOnValueRatioChange(UnityAction<float> action)
        {
            OnValueRatioChange.AddListener(action);
        }
        public void StopListeningOnValueRatioChange(UnityAction<float> action)
        {
            OnValueRatioChange.RemoveListener(action);
        }
    }
}