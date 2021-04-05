using System;
using UnityEngine;

namespace BallGame
{
    [Serializable]
    public class ObjectSavedData : SavedData
    {
        public Vector3Serializable Rotation;

        public override string ToString() => $"Name {Name} Position {Position} IsVisible {IsEnabled}";
    }
}