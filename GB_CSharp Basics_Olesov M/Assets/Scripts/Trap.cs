using UnityEngine;

namespace BallGame
{
    public class Trap : InteractiveObject
    {
        private Material _material;

        protected void Start()
        {
            _material = GetComponent<Renderer>().material;
            _material.color = Color.magenta;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsInteractable || !other.gameObject == _player.gameObject)
            {
                return;
            }
            if (!_player.CheckInvincibility())
                Interaction();
        }

        protected override void Interaction()
        {
            
        }
    }
}
