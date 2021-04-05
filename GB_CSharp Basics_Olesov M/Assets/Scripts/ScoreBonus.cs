using UnityEngine;

namespace BallGame
{
    public sealed class ScoreBonus : GoodBonus
    { 
        [SerializeField] private int _score = 100;

        public delegate void ScorePickUp(int score);
        public event ScorePickUp GetBonusScore;

        private new void Start()
        {
            base.Start();
            _material.color = Color.green;
        }

        protected override void Interaction()
        {
            TriggerEvent();
            GetBonusScore?.Invoke(_score);
        }
    }
}
