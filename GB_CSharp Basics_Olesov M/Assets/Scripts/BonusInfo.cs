using System;

namespace BallGame
{
    [Serializable]
    public struct BonusInfo
    {
        public float scoreMultiplier;
        public float scoreMultiplierCountDown;
        public float currentSpeed;

        public float speedUpModifier;
        public float slowDownModifier;
        public float speedUpDuration;
        public float slowDownDuration;

        public bool isInvincible;
        public float invincibilityDuration;

        public BonusInfo(float scoreMultiplier, float scoreMultiplierCountDown, float currentSpeed,
            float speedUpModifier, float slowDownModifier, float speedUpDuration, float slowDownDuration,
            bool isInvincible, float invincibilityDuration)
        {
            this.scoreMultiplier = scoreMultiplier;
            this.scoreMultiplierCountDown = scoreMultiplierCountDown;
            this.currentSpeed = currentSpeed;
            this.speedUpModifier = speedUpModifier;
            this.slowDownModifier = slowDownModifier;
            this.speedUpDuration = speedUpDuration;
            this.slowDownDuration = slowDownDuration;
            this.isInvincible = isInvincible;
            this.invincibilityDuration = invincibilityDuration;
        }

        public override string ToString() => $"Score Multiplier {scoreMultiplier}, Score Multiplier CD {scoreMultiplierCountDown}," +
            $"Current Speed {currentSpeed}, Speed Up {speedUpModifier}, Slow Down {slowDownModifier}, Speed Up Duration {speedUpDuration}," +
            $"Slow Down Duration {slowDownDuration}, Is Invincible {isInvincible}, Invincibility Duration {invincibilityDuration}";
    }
}
