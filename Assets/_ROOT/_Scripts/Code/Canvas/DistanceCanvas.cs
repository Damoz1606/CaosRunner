using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UICanvas
{
    public class DistanceCanvas : MonoBehaviour
    {
        [SerializeField] private SO.SODistance distance;
        private TMPro.TextMeshProUGUI distanceText;
        void Awake()
        {
            distanceText = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            distance.StartListeningDistanceChange(UpdateUI);
        }

        private void OnDisable()
        {
            distance.StopListeningDistanceChange(UpdateUI);
        }

        private void UpdateUI(float distance)
        {
            string distanceTxt = distance.ToString("0");
            distanceText.text = $"{distanceTxt} m";
        }
    }
}
