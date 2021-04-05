using UnityEngine;

namespace BallGame
{
    public sealed class SpringTrap : Trap
    {
        [SerializeField] private float _force =5.0f;

        private new void Start()
        {
            base.Start();
            transform.rotation = Quaternion.Euler(0,Random.Range(0,360),0);
        }

        protected override void Interaction()
        {
            Vector3 direction = transform.forward + (transform.up*2);

            _playerRigidBody.velocity = new Vector3(0,0,0);
            _playerRigidBody.AddForce(direction * _force, ForceMode.VelocityChange);
        }
    }
}