using System.Collections;
using System.Collections.Generic;
using SO.Variables;
using UnityEngine;

namespace Player
{
    public class Combo : MonoBehaviour
    {
        [SerializeField] private float seconds = 2;

        [SerializeField] private FloatSO combo;

        [SerializeField] private float timeRemaining = 0;

        [SerializeField] private float maxCombo = 0;

        private bool hasTimerEnd = true;

        private void Start()
        {
            combo.Init();
        }

        private void OnEnable()
        {
            combo.StartListeningOnValueChange(OnComboChange);
        }

        private void OnDisable()
        {
            combo.StopListeningOnValueChange(OnComboChange);
        }

        private void OnComboChange(float value)
        {
            if (value <= 0) return;
            if (value > maxCombo) maxCombo = value;
            timeRemaining = seconds;
            if (hasTimerEnd)
            {
                combo.Init();
                hasTimerEnd = false;
            }
        }

        private void Update()
        {
            if (hasTimerEnd) return;
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0.0f)
            {
                combo.Init();
                hasTimerEnd = true;
            }

        }
    }

}