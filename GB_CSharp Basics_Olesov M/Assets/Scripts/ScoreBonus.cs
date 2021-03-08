using UnityEngine;

namespace BallGame
{
    public sealed class ScoreBonus : GoodBonus
    { 
        [SerializeField] private int _score = 100;
        private ScoreTracker _scoreTracker;

        private new void Start()
        {
            base.Start();
            _scoreTracker = FindObjectOfType<ScoreTracker>();
            _material.color = Color.green;
        }

        protected override void Interaction()
        {
            _scoreTracker.AddScore(_score);
        }
    }
}
