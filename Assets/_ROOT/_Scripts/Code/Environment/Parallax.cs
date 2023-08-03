using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enviroment
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private float depth = 1;

        private Player.Run runner;

        private void Awake()
        {
            bool hasRunner = GameObject.Find("Player").TryGetComponent<Player.Run>(out runner);
            if (!hasRunner) throw new System.Exception("There is no runner in the scene");
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float realVelocity = runner.Velocity / depth;
            Vector2 position = transform.position;
            position.x -= realVelocity * Time.fixedDeltaTime;

            if(position.x < -12) {
                position.x = 22;
            }

            transform.position = position;
        }
    }

}