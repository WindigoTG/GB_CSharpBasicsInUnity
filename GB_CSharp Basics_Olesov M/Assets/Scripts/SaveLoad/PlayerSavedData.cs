using System;

namespace BallGame
{
    [Serializable]
    public class PlayerSavedData : SavedData
    {
        public Vector3Serializable Velocity;
        public BonusInfo Bonuses;

        public override string ToString() => $"Name {Name} Position {Position} IsVisible {IsEnabled} Velocity {Velocity}";
    }
}