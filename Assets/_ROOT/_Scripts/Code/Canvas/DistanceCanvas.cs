using System.Collections;
using System.Collections.Generic;
using SO.Variables;
using UnityEngine;

namespace UICanvas
{
    public class DistanceCanvas : MonoBehaviour
    {
        [SerializeField] private FloatSO distance;
        private TMPro.TextMeshProUGUI distanceText;
        void Awake()
        {
            distanceText = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            distance.StartListeningOnValueChange(UpdateUI);
        }

        private void OnDisable()
        {
            distance.StartListeningOnValueChange(UpdateUI);
        }

        private void UpdateUI(float distance)
        {
            string distanceTxt = distance.ToString("0");
            distanceText.text = $"{distanceTxt} m";
        }
    }
}
