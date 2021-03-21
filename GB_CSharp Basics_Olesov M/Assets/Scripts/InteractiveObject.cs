using System;
using UnityEngine;

namespace BallGame
{
    public abstract class InteractiveObject : MonoBehaviour, IInteractable, ICloneable
    {
        public bool IsInteractable { get; } = true;

        protected Player _player;

        public delegate void InteractedByPlayer();
        public event InteractedByPlayer PlayerInteraction;

        protected abstract void Interaction();

        private void Awake()
        {
            _player = FindObjectOfType<Player>();
        }


        void IActionable.Action()
        {
            if (TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = UnityEngine.Random.ColorHSV();
            }
        }

        void IInitializable.Action()
        {
            if (TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = Color.cyan;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsInteractable || !other.gameObject == _player.gameObject)
            {
                return;
            }
            Interaction();
            Destroy(gameObject);
        }

        public object Clone()
        {
            var result = Instantiate(gameObject, transform.position, transform.rotation, transform.parent);
            return result;
        }

        protected void TriggerEvent()
        {
            PlayerInteraction?.Invoke();
        }
    }

}

