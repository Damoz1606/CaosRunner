using System.Collections;
using System.Collections.Generic;
using SO;
using UnityEngine;
using UnityEngine.UI;

namespace UICanvas
{
    public class SpeedometerCanvas : MonoBehaviour
    {
        [SerializeField] private float delta = 2;

        private float currentValue = 0;
        private float speed = 0;

        private Slider slider;

        public SORun runner;

        private void Awake()
        {
            bool hasSlider = TryGetComponent<Slider>(out slider);
            if (!hasSlider) slider = gameObject.AddComponent<Slider>();
        }

        private void Start()
        {
            slider.value = 0;
        }

        private void Update()
        {
            currentValue = Mathf.MoveTowards(currentValue, speed, delta);
            slider.value = currentValue;
        }

        private void OnEnable()
        {
            runner.OnVelocityRatioChange.AddListener(SetSpeed);
        }

        private void OnDisable()
        {
            runner.OnVelocityRatioChange.AddListener(SetSpeed);
        }

        private void SetSpeed(float value)
        {
            speed = value;
        }
    }
}