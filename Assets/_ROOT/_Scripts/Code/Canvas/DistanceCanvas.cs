using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UICanvas
{
    public class DistanceCanvas : MonoBehaviour
    {
        private Player.Run runner;
        private TMPro.TextMeshProUGUI distanceText;
        void Awake()
        {
            runner = GameObject.Find("Player").GetComponent<Player.Run>();
            if (runner == null) throw new System.Exception("There is no runner in the scene");

            distanceText = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            runner.OnDistanceChange += UpdateUI;
        }

        private void OnDisable()
        {
            runner.OnDistanceChange -= UpdateUI;
        }

        private void UpdateUI(float distance)
        {
            string distanceTxt = distance.ToString("0");
            distanceText.text = $"{distanceTxt} m";
        }
    }
}
