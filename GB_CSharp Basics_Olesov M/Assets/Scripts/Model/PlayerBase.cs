using System;
using System.Collections;
using UnityEngine;

namespace BallGame
{
    public abstract class PlayerBase : MonoBehaviour
    {
        [SerializeField] protected float _defaultSpeed = 3.0f;

        protected float _speed;

        protected Rigidbody _rigidbody;
        protected Material _material;

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            _rigidbody = GetComponent<Rigidbody>();
        }

        public abstract void Move(float x, float y, float z);
     }
}