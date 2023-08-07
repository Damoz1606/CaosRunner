using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    public class SOJump : ScriptableObject
    {
        [SerializeField] private float minJumpTime = 0.15f;
        [SerializeField] private float maxJumpTime = 0.35f;
        [SerializeField] private float force = 1f;

        public float Force { get => force; }
        public float MinJumpTime { get => minJumpTime; }
        public float MaxJumpTime { get => maxJumpTime; }
    }
}
