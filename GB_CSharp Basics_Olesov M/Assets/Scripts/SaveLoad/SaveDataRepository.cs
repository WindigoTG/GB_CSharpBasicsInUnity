using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BallGame
{
    public sealed class SaveDataRepository
    {
        private readonly IData<GameSavedData> _data;

        private const string _folderName = "dataSave";
        private const string _fileName = "data.bat";
        private readonly string _path;

        public SaveDataRepository()
        {
            //_data = new JsonData<GameSavedData>();
            _data = new BinarySerializationData<GameSavedData>();
            //_data = new SerializableXMLData<GameSavedData>();

            _path = Path.Combine(Application.dataPath, _folderName);
        }

        public void Save(PlayerBall player, List<InteractiveObject> intObjs, GameStatsInfo gameStats)
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }

            var savedData = new GameSavedData
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

            _data.Save(savedData, Path.Combine(_path, _fileName));
        }

        public void Load(PlayerBall player, List<InteractiveObject> intObjs, ref GameStatsInfo gameStats)
        {
            var file = Path.Combine(_path, _fileName);
            if (!File.Exists(file)) return;

            var newData = _data.Load(file);
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
    }
}