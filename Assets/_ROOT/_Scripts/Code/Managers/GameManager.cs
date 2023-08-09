using Player;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        private GameObject player;

        public GameObject Player { get => player; }

        private void Awake()
        {
            player = GameObject.Find("Player");
        }
    }
}
