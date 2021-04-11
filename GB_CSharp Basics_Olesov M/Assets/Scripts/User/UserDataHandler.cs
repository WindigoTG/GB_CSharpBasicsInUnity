using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallGame
{
    public sealed class UserDataHandler
    {
        private List<UserData> _registeredUsers;
        private List<ResultEntry> _topTenResults;
        private SaveDataRepository _saveDataRepository;

        private int _selectedUserIndex;

        public UserDataHandler(SaveDataRepository repository)
        {
            _saveDataRepository = repository;
            _topTenResults = new List<ResultEntry>();
            _registeredUsers = new List<UserData>();
            _saveDataRepository.LoadUserData(_registeredUsers);
        }

        public UserDataHandler()
        {
            _registeredUsers = new List<UserData>();
            _topTenResults = new List<ResultEntry>();
        }

        public UserDataHandler(List<UserData> users)
        {
            _registeredUsers = users;
            _topTenResults = new List<ResultEntry>();
        }

        private void FillInTopTen()
        {
            if (_registeredUsers.Count > 0)
            {
                for (int i = 0; i < _registeredUsers.Count; i++)
                {
                    for (int j = 0; j < _registeredUsers[i].Results.Count; j++)
                        _topTenResults.Add(new ResultEntry(_registeredUsers[i].UserName, _registeredUsers[i].Results[j]));
                }
                _topTenResults.Sort();
                if (_topTenResults.Count > 10)
                    _topTenResults.RemoveRange(10, _topTenResults.Count - 10);
            }
        }

        public int UserCount => _registeredUsers.Count;

        public void AddUser(string userName)
        {
            _registeredUsers.Add(new UserData(userName));
            _saveDataRepository.SaveUserData(_registeredUsers);
        }

        public void RemoveUser(UserData user)
        {
            _registeredUsers.Remove(user);
            _saveDataRepository.SaveUserData(_registeredUsers);
        }

        public void SetSelectedUser(int index)
        {
            _selectedUserIndex = index;
        }

        public int SelectedUser => _selectedUserIndex;

        public UserData this[int index]
        {
            get
            {
                if (index >= 0 && index < _registeredUsers.Count)
                    return _registeredUsers[index];
                else
                    return null;
            }
        }

        public void AddResult(ResultData result)
        {
            _registeredUsers[_selectedUserIndex].AddResult(result);
            _saveDataRepository.SaveUserData(_registeredUsers);
            FillInTopTen();
        }

        public List<ResultEntry> TopTen => _topTenResults;
    }
}
