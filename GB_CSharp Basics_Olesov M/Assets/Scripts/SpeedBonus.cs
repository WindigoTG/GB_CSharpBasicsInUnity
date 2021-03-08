using UnityEngine;

namespace BallGame
{
    public sealed class SpeedBonus : GoodBonus
    {
        [SerializeField] private float _speedBonus = 2.0f;
        [SerializeField] private float _bonusDuration = 15.0f;

        
        protected override void Interaction()
        {
            if (_speedBonus < 0)
                _speedBonus = -_speedBonus;
            if (_bonusDuration < 0)
                _bonusDuration = -_bonusDuration;
            _player.ModifySpeed(_speedBonus, _bonusDuration);
        }

    }
}
