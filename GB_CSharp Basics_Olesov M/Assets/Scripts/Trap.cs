using UnityEngine;

namespace BallGame
{
    public class Trap : InteractiveObject
    {
        private Material _material;
        protected Rigidbody _playerRigidBody;

        protected void Start()
        {
            _material = GetComponent<Renderer>().material;
            _material.color = Color.magenta;

            if (_player != null)
                _playerRigidBody = _player.gameObject.GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_material != null)
            {
                if (!IsInteractable || !other.gameObject == _player.gameObject)
                {
                    return;
                }
                if (!_player.IsInvincible)
                    Interaction();
            }
        }

        protected override void Interaction()
        {
            
        }

        public override void Execute() { }
    }
}
