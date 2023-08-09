using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class MasterManager : MonoBehaviour
    {
        public static MasterManager SharedInstance;

        public GameManager GameManager { get; private set; }
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
            GameManager = GetComponentInChildren<GameManager>();
            AudioManager = GetComponentInChildren<AudioManager>();
            UIManager = GetComponentInChildren<UIManager>();
        }
    }
}
