using UnityEngine;

namespace BallGame
{
    public sealed class CameraController : MonoBehaviour
    {
        [SerializeField] private Player _player;
        private Vector3 _offset;

        private void Start()
        {
            _offset = transform.position - _player.transform.position;
        }

        private void LateUpdate()
        {
            transform.position = _player.transform.position + _offset;
        }
    }
}

