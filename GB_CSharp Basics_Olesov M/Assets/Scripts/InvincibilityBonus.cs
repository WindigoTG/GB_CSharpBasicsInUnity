using UnityEngine;

namespace BallGame
{
    public sealed class InvincibilityBonus : GoodBonus
    {
        [SerializeField] private float _bonusDuration = 15.0f;

        private new void Start()
        {
            base.Start();
            _material.color = Color.cyan;
        }

        protected override void Interaction()
        {
            if (_bonusDuration < 0)
                _bonusDuration = -_bonusDuration;
            _player.GetInvincibility(_bonusDuration);
        }
    }
}
