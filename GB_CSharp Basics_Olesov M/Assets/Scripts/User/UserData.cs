using System;
using System.Collections.Generic;

namespace BallGame
{
    [Serializable]
    public sealed class UserData : IComparable<UserData>
    {
        private string _name;
        private List<ResultData> _results;

        public UserData(string userName)
        {
            _name = userName;
            _results = new List<ResultData>();
        }

        public string UserName
        {
            get => _name;
        }
            
        public List<ResultData> Results
        {
            get => _results;
        }

        public void AddResult(ResultData result)
        {
            _results.Add(result);
            _results.Sort();
            if (_results.Count > 10)
                _results.RemoveRange(10, _results.Count - 10);
        }

        public int CompareTo(UserData other)
        {
            return _name.CompareTo(other._name);
        }

        public void Deconstruct(out string userName, out List<ResultData> results) =>
                (userName, results) = (_name, _results);
    }
}
