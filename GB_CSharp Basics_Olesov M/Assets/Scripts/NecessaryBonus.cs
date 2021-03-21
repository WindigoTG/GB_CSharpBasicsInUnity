using UnityEngine;

namespace BallGame
{
    public sealed class NecessaryBonus : GoodBonus
    {
        private new void Start()
        {
            base.Start();
            _material.color = Color.yellow;
        }

        protected override void Interaction()
        {
            TriggerEvent();
        }
    }
}
