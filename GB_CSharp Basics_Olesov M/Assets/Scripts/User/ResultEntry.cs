using System;

namespace BallGame
{
    public sealed class ResultEntry : IComparable<ResultEntry>
    {
        private string _userName;
        private int _time;
        private int _score;

        public ResultEntry(string userName, ResultData result)
        {
            _userName = userName;
            _time = result.Time;
            _score = result.Score;
        }

        public int CompareTo(ResultEntry other)
        {
            if (_time > other._time) return 1;
            if (_time < other._time) return -1;
            if (_score < other._score) return 1;
            if (_score > other._score) return -1;
            return _userName.CompareTo(other._userName);
        }

        public string UserName => _userName;

        public void Deconstruct(out string userName, out int time, out int score) =>
                (userName, time, score) = (_userName, _time, _score);
    }
}