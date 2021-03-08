
using UnityEngine;

namespace BallGame
{
    public sealed class SlowFieldTrap : Trap
    {
        [SerializeField] private float _slowDownForce = 5.0f;

        private Rigidbody _playerRigidBody;

        private new void Start()
        {
            base.Start();
            _playerRigidBody = _player.gameObject.GetComponent<Rigidbody>();
        }

        protected override void Interaction()
        {
            _playerRigidBody.velocity /= _slowDownForce;
            _playerRigidBody.mass = _slowDownForce;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == _player.gameObject)
                _playerRigidBody.mass = 1.0f;
        }
    }
}
