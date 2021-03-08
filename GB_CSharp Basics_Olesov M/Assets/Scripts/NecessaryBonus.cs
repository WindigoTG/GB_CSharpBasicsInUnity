using UnityEngine;

namespace BallGame
{
    public sealed class NecessaryBonus : GoodBonus
    {
        private ScoreTracker _scoreTracker;

        private new void Start()
        {
            base.Start();
            _scoreTracker = FindObjectOfType<ScoreTracker>();
            _material.color = Color.yellow;
        }

        protected override void Interaction()
        {
            _scoreTracker.GetNeededItem();
        }
    }
}
