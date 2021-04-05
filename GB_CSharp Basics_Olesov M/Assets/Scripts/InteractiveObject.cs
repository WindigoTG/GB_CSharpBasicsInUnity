using System;
using UnityEngine;

namespace BallGame
{
    public abstract class InteractiveObject : MonoBehaviour, IInteractable, ICloneable, IExecuteable
    {
        //public bool IsInteractable { get; } = true;

        protected PlayerBall _player;

        public delegate void InteractedByPlayer();
        public event InteractedByPlayer PlayerInteraction;

        private bool _isInteractable;

        public bool IsInteractable
        {
            get { return _isInteractable; }
            private set
            {
                _isInteractable = value;
                GetComponent<Renderer>().enabled = _isInteractable;
                GetComponent<Collider>().enabled = _isInteractable;
            }
        }


        protected abstract void Interaction();

        private void Awake()
        {
            IsInteractable = true;
        }

        public void RegisterPlayer(PlayerBall player)
        {
            _player = player;
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
            IsInteractable = false;
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

        public abstract void Execute();
    }

}

