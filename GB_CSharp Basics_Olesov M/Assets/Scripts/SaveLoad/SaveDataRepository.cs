using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BallGame
{
    public sealed class SaveDataRepository
    {
        private readonly IData<SavedGamedData> _currentGameData;
        private readonly IData<SavedUsersData> _userData;

        private const string _folderName = "dataSave";
        private const string _saveGameFileName = "data.bat";
        private const string _userDataFileName = "userData.bat";
        private readonly string _path;

        public SaveDataRepository()
        {
            //_data = new JsonData<GameSavedData>();
            _currentGameData = new BinarySerializationData<SavedGamedData>();
            //_data = new SerializableXMLData<GameSavedData>();

            _userData = new BinarySerializationData<SavedUsersData>();

            _path = Path.Combine(Application.dataPath, _folderName);
        }

        public void Save(PlayerBall player, List<InteractiveObject> intObjs, GameStatsInfo gameStats)
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }

            var savedData = new SavedGamedData
            {
                Player = new PlayerSavedData
                {
                    Position = player.transform.position,
                    Name = player.name,
                    IsEnabled = true,
                    Velocity = player.GetComponent<Rigidbody>().velocity,
                    Bonuses = player.CurrentBonusInfo
                },
                Objects = new List<ObjectSavedData>(),
                Stats = gameStats
            };

            for (int i = 0; i < intObjs.Count; i++)
            {
                savedData.Objects.Add(
                    new ObjectSavedData
                    {
                        Position = intObjs[i].transform.position,
                        Name = intObjs[i].name,
                        IsEnabled = intObjs[i].GetComponent<InteractiveObject>().IsInteractable,
                        Rotation = intObjs[i].transform.rotation.eulerAngles
                    });
            }

            _currentGameData.Save(savedData, Path.Combine(_path, _saveGameFileName));
        }

        public void Load(PlayerBall player, List<InteractiveObject> intObjs, ref GameStatsInfo gameStats)
        {
            var file = Path.Combine(_path, _saveGameFileName);
            if (!File.Exists(file)) return;

            var newData = _currentGameData.Load(file);
            player.transform.position = newData.Player.Position;
            player.name = newData.Player.Name;
            player.gameObject.SetActive(newData.Player.IsEnabled);
            player.GetComponent<Rigidbody>().velocity = newData.Player.Velocity;
            player.CurrentBonusInfo = newData.Player.Bonuses;

            gameStats = newData.Stats;

            for (int i = 0; i < intObjs.Count; i++)
            {
                intObjs[i].transform.position = newData.Objects[i].Position;
                intObjs[i].name = newData.Objects[i].Name;
                intObjs[i].GetComponent<InteractiveObject>().IsInteractable = newData.Objects[i].IsEnabled;
                intObjs[i].transform.rotation = Quaternion.Euler(newData.Objects[i].Rotation);
            }
        }

        public void SaveUserData(List<UserData> users)
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }

            var saveData = new SavedUsersData { Users = new List<UserData>()};
            for (int i = 0; i < users.Count; i++)
                saveData.Users.Add(users[i]);

            _userData.Save(saveData, Path.Combine(_path, _userDataFileName));
        }

        public void LoadUserData(List<UserData> users)
        {
            var file = Path.Combine(_path, _userDataFileName);
            if (!File.Exists(file)) return;

            var newData = _userData.Load(file);
            for (int i = 0; i < newData.Users.Count; i++)
                users.Add(newData.Users[i]);
        }
    }
}