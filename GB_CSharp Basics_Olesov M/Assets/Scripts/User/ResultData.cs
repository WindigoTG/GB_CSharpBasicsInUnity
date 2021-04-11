using System;

namespace BallGame
{
    [Serializable]
    public sealed class ResultData : IComparable<ResultData>
    {
        private int _time;
        private int _score;

        public int Score
        {
            get => _score;
            set
            {
                if (value > 0) _score = value;
                else _score = 0;
            }
        }

        public int Time
        {
            get => _time;
            set
            {
                if (value > 0 && value < 215999) _time = value;
                else _time = 215999;
            }
        }

        public int CompareTo(ResultData other)
        {
            if (_time > other._time) return 1;
            if (_time < other._time) return -1;
            return -_score.CompareTo(other._score);
        }

        public void Deconstruct(out int time, out int score) =>
                (time, score) = (_time, _score);
    }
}
