using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BallGame
{
    public class SpawnPointPlacementWindow : EditorWindow
    {
        private SpawnPoints selectedObject;
        private bool _isPlacing;
        private GameObject _spawnPointCreator;

        private List<GameObject> _createdSpawnPoints;
        private int _totalCount;
        private int _necessaryCount;
        private int _scoreCount;
        private int _speedCount;
        private int _slowCount;
        private int _invincibilityCount;

        private bool _isGroupEnabled;

        private void OnGUI()
        {
            selectedObject = (SpawnPoints)EditorGUILayout.EnumPopup("SpawnPoint type", selectedObject);

            EditorGUILayout.Space();
            if (!_isPlacing)
            {
                if (GUILayout.Button("Begin placement"))
                {
                    _spawnPointCreator = new GameObject("SpawnPointCreator");
                    _spawnPointCreator.AddComponent<SpawnPointPlacement>().SetSource(this);
                    Selection.activeObject = _spawnPointCreator;
                    _totalCount = 0;
                    _necessaryCount = 0;
                    _scoreCount = 0;
                    _speedCount = 0;
                    _slowCount = 0;
                    _invincibilityCount = 0;
                    _isPlacing = true;
                }
            }
            else
            {
                if (GUILayout.Button("End placement"))
                {
                    if (_spawnPointCreator)
                        DestroyImmediate(_spawnPointCreator);
                    _createdSpawnPoints.Clear();
                    _totalCount = 0;
                    _necessaryCount = 0;
                    _scoreCount = 0;
                    _speedCount = 0;
                    _slowCount = 0;
                    _invincibilityCount = 0;
                    _isPlacing = false;
                    _isGroupEnabled = false;
                }

                EditorGUILayout.Space();
                if (GUILayout.Button("Remove last"))
                    if (_totalCount > 0)
                        RemoveSpawnPoint(_createdSpawnPoints[_totalCount-1]);
                EditorGUILayout.Space();

                _isGroupEnabled = EditorGUILayout.BeginToggleGroup("Remove by type", _isGroupEnabled);
                if (GUILayout.Button("Remove last Necessary"))
                    if (_necessaryCount > 0)
                    {
                        List<GameObject> targets = new List<GameObject>();
                        for (int i = 0; i < _totalCount; i++)
                            if (_createdSpawnPoints[i].CompareTag("NecessaryBonusSpawn"))
                                targets.Add(_createdSpawnPoints[i]);
                        RemoveSpawnPoint(targets[_necessaryCount - 1]);
                    }
                if (GUILayout.Button("Remove last Score"))
                    if (_scoreCount > 0)
                    {
                        List<GameObject> targets = new List<GameObject>();
                        for (int i = 0; i < _totalCount; i++)
                            if (_createdSpawnPoints[i].CompareTag("ScoreBonusSpawn"))
                                targets.Add(_createdSpawnPoints[i]);
                        RemoveSpawnPoint(targets[_scoreCount - 1]);
                    }
                if (GUILayout.Button("Remove last Speed"))
                    if (_speedCount > 0)
                    {
                        List<GameObject> targets = new List<GameObject>();
                        for (int i = 0; i < _totalCount; i++)
                            if (_createdSpawnPoints[i].CompareTag("SpeedBonusSpawn"))
                                targets.Add(_createdSpawnPoints[i]);
                        RemoveSpawnPoint(targets[_speedCount - 1]);
                    }
                if (GUILayout.Button("Remove last Slow"))
                    if (_slowCount > 0)
                    {
                        List<GameObject> targets = new List<GameObject>();
                        for (int i = 0; i < _totalCount; i++)
                            if (_createdSpawnPoints[i].CompareTag("SlowBonusSpawn"))
                                targets.Add(_createdSpawnPoints[i]);
                        RemoveSpawnPoint(targets[_slowCount - 1]);
                    }
                if (GUILayout.Button("Remove last Invincibility"))
                    if (_invincibilityCount > 0)
                    {
                        List<GameObject> targets = new List<GameObject>();
                        for (int i = 0; i < _totalCount; i++)
                            if (_createdSpawnPoints[i].CompareTag("InvincibilityBonusSpawn"))
                                targets.Add(_createdSpawnPoints[i]);
                        RemoveSpawnPoint(targets[_invincibilityCount - 1]);
                    }
                EditorGUILayout.EndToggleGroup();
            }
        }

        public void RegisterCreatedSpawnPoint(GameObject spawnPoint)
        {
            _createdSpawnPoints.Add(spawnPoint);
            _totalCount++;
            if (spawnPoint.CompareTag("NecessaryBonusSpawn"))
                _necessaryCount++;
            if (spawnPoint.CompareTag("ScoreBonusSpawn"))
                _scoreCount++;
            if (spawnPoint.CompareTag("SpeedBonusSpawn"))
                _speedCount++;
            if (spawnPoint.CompareTag("SlowBonusSpawn"))
                _slowCount++;
            if (spawnPoint.CompareTag("InvincibilityBonusSpawn"))
                _invincibilityCount++;

            Debug.Log(_totalCount + "|" + _necessaryCount + "|" + _scoreCount + "|" + _speedCount + "|" + _slowCount + "|" + _invincibilityCount);
        }

        private void RemoveSpawnPoint(GameObject spawnPoint)
        {
            if (spawnPoint.CompareTag("NecessaryBonusSpawn"))
                _necessaryCount--;
            if (spawnPoint.CompareTag("ScoreBonusSpawn"))
                _scoreCount--;
            if (spawnPoint.CompareTag("SpeedBonusSpawn"))
                _speedCount--;
            if (spawnPoint.CompareTag("SlowBonusSpawn"))
                _slowCount--;
            if (spawnPoint.CompareTag("InvincibilityBonusSpawn"))
                _invincibilityCount--;
            _totalCount--;
            _createdSpawnPoints.Remove(spawnPoint);
            DestroyImmediate(spawnPoint);
            Debug.Log(_totalCount + "|" + _necessaryCount + "|" + _scoreCount + "|" + _speedCount + "|" + _slowCount + "|" + _invincibilityCount);
        }

        public string SelectedSpawnPoint => selectedObject.ToString();
    }

    public enum SpawnPoints
    {
        NecessaryBonusSpawn,
        ScoreBonusSpawn,
        SpeedBonusSpawn,
        SlowBonusSpawn,
        InvincibilityBonusSpawn
    }
}