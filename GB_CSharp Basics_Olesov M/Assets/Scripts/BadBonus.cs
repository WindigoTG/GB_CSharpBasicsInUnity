using UnityEngine;

namespace BallGame
{
    public sealed class BadBonus : InteractiveObject, ILevitateable, IRotatable
    {
        [SerializeField] private float _speedPenalty = -0.5f;
        [SerializeField] private float _penaltyDuration = 15.0f;

        private float _speedRotation;
        private float _levitationHeight;
        private Material _material;

        

        private void Start()
        {
            _material = GetComponent<Renderer>().material;
            _speedRotation = Random.Range(10.0f, 50.0f);
            _levitationHeight = Random.Range(1.0f, 2.0f);
            _material.color = Color.red;
        }

        protected override void Interaction()
        {
            if (_speedPenalty > 0)
                _speedPenalty = -_speedPenalty;
            if (_penaltyDuration < 0)
                _penaltyDuration = -_penaltyDuration;
            _player.ModifySpeed(_speedPenalty, _penaltyDuration);

            TriggerEvent();
        }

        public override void Execute()
        {
            if (!IsInteractable) { return; }
            Levitate();
            Rotation();
        }

        public void Levitate()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 
                                                 Mathf.PingPong(Time.time, _levitationHeight), 
                                                 transform.localPosition.z);
        }

        public void Rotation()
        {
            transform.Rotate(Vector3.up * (Time.deltaTime * _speedRotation), Space.World);
        }
    }
}
