using System;
using UnityEngine;

namespace BallGame
{
    public sealed class CameraController : IExecuteable
    {
        private Transform _player;
        private Transform _mainCamera;
        private Vector3 _offset;

        [SerializeField] private float _shakeDuration = 0f;
        [SerializeField] private float _shakeAmount = 0.025f;
        [SerializeField] private float _decreaseFactor = 1.0f;

        public CameraController(Transform player, Transform mainCamera)
        {
            _player = player;
            _mainCamera = mainCamera;
            _mainCamera.LookAt(_player);
            _offset = _mainCamera.position - _player.position;
        }

        public void Shake()
        {
            _shakeDuration = 0.5f;
        }

        public void Execute()
        {
            if (_shakeDuration > 0)
            {
                _mainCamera.position = _player.transform.position + _offset + UnityEngine.Random.insideUnitSphere * _shakeAmount;

                _shakeDuration -= Time.deltaTime * _decreaseFactor;
            }
            else
            {
                _shakeDuration = 0f;
                _mainCamera.position = _player.transform.position + _offset;
            }
        }
    }
}

