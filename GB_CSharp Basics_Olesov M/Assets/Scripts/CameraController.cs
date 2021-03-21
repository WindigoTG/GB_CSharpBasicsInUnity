using System;
using UnityEngine;

namespace BallGame
{
    public sealed class CameraController : MonoBehaviour
    {
        [SerializeField] private Player _player;
        private Vector3 _offset;

        [SerializeField] private float _shakeDuration = 0f;
        [SerializeField] private float _shakeAmount = 0.025f;
        [SerializeField] private float _decreaseFactor = 1.0f;

        private void Start()
        {
            if (_player == null)
                throw new Exception("Camera must have a player to follow set up manually in editor");

            _offset = transform.position - _player.transform.position;
        }

        private void LateUpdate()
        {
            if (_player != null)
            {
                if (_shakeDuration > 0)
                {
                    transform.position = _player.transform.position + _offset + UnityEngine.Random.insideUnitSphere * _shakeAmount;

                    _shakeDuration -= Time.deltaTime * _decreaseFactor;
                }
                else
                {
                    _shakeDuration = 0f;
                    transform.position = _player.transform.position + _offset;
                }
            }
        }

        public void Shake()
        {
            _shakeDuration = 0.5f;
        }
    }
}

