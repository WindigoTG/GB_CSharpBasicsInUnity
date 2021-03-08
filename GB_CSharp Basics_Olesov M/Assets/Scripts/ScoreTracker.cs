
using UnityEngine;

namespace BallGame
{
    public class ScoreTracker : MonoBehaviour
    {
        private int _score;
        private int _neededItems;

        public int Score
        {
            get { return _score; }
        }

        public int NeededItems
        { get { return _neededItems; } }

        public void AddScore(int points)
        {
            if (points > 0)
                _score += points;
        }

        public void GetNeededItem()
        { _neededItems++; }
    }
}