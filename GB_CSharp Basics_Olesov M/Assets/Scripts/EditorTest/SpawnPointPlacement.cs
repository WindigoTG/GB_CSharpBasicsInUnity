using UnityEngine;

namespace BallGame
{
    public class SpawnPointPlacement : MonoBehaviour
    {
        private GameObject _prefab;
        private Transform _root;
        private SpawnPointPlacementWindow _sourceWindow;

        public void SetSource(SpawnPointPlacementWindow source)
        {
            _sourceWindow = source;
        }

        public void InstantiateObj(Vector3 pos)
        {
            string path = "SpawnPoints/" + _sourceWindow.SelectedSpawnPoint;
            _prefab = Resources.Load<GameObject>(path);
            if (!_root)
            {
                _root = GameObject.Find("BonusSpawnPoints").transform;
                if (!_root)
                {
                    _root = new GameObject("BonusSpawnPoints").transform;
                }
            }

            if (_prefab != null)
            {
                _sourceWindow.RegisterCreatedSpawnPoint(Instantiate(_prefab, pos, Quaternion.identity, _root));
            }
        }

    }
}