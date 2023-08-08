using System.Collections;
using System.Collections.Generic;
using SO.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace UICanvas
{
    public class ComboCanvas : MonoBehaviour
    {
        [SerializeField] private FloatSO combo;

        private TMPro.TextMeshProUGUI text;

        private void Awake()
        {
            text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            combo.StartListeningOnValueChange(OnChangeComboValue);
        }

        private void OnDisable()
        {
            combo.StopListeningOnValueChange(OnChangeComboValue);
        }

        private void OnChangeComboValue(float value)
        {
            if (value <= 0)
                text.gameObject.SetActive(false);
            else
            {
                if (!text.gameObject.activeInHierarchy)
                {
                    text.gameObject.SetActive(true);
                }
                text.text = $"{value:0} Combo";
            }
        }
    }
}