using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class MasterManager : MonoBehaviour
    {
        public static MasterManager SharedInstance;

        public UIManager UIManager { get; private set; }
        public AudioManager AudioManager { get; private set; }

        private void Awake()
        {
            if (SharedInstance != null && SharedInstance != this)
            {
                Destroy(this);
                return;
            }
            SharedInstance = this;
            AudioManager = GetComponentInChildren<AudioManager>();
            UIManager = GetComponentInChildren<UIManager>();
        }
    }
}
