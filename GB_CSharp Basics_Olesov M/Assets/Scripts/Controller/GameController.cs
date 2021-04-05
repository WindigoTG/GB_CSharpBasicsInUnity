using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BallGame
{
    public class GameController : MonoBehaviour
    {
        private BonusHandler _bonuses;
        private ScoreTracker _scoreTracker;

        private int _elapsedTimeOnLoad = 0;
        private int _currentElapsedTime;
        private int _timeOfLastLoad = 0;

        private string _currentTime;

        private GameObject _player;
        private bool _isPlayerAtFinish;
        private int _necessaryBonusTotalCount;

        private CameraController _cameraController;
        private InputController _inputController;
        private ExecuteableObjectsList _executableObjects;
        private MiniMapController _miniMapController;
        private RadarController _radarController;

        private SaveDataRepository _saveDataRepository;

        [SerializeField] Texture _necessaryBonusImage;

        private bool _isUsingAlternativeControl;

        private bool _isGamePaused;

        private void Awake()
        {
            _scoreTracker = new ScoreTracker();

            var reference = new Reference();
            _cameraController = new CameraController(reference.PlayerBall.transform, reference.MainCamera.transform);
            _miniMapController = new MiniMapController(reference.PlayerBall.transform, reference.MiniMapCamera.transform);
            _radarController = new RadarController(reference.PlayerBall.transform, reference.Radar.transform);
            _player = reference.PlayerBall.gameObject;
            _inputController = new InputController(reference.PlayerBall);
            _saveDataRepository = new SaveDataRepository();

            _inputController.ChangeControlMethod += ChangeControl;
            _inputController.PauseGame += PauseGame;
            _inputController.SaveGame += SaveGame;
            _inputController.LoadGame += LoadGame;

            _player.GetComponent<PlayerBall>().GetBonusScore += _scoreTracker.AddScore;

            SetUpGame(reference);
            _executableObjects = new ExecuteableObjectsList();

            _executableObjects.AddExecuteableObject(_cameraController);
            _executableObjects.AddExecuteableObject(_miniMapController);
            _executableObjects.AddExecuteableObject(_radarController);

            _necessaryBonusTotalCount = FindObjectsOfType<NecessaryBonus>().Length;

            if (_necessaryBonusImage == null)
                throw new Exception("An image for Necessary Bonus in GameController must be set up manually");
        }

        void Update()
        {
            _currentElapsedTime = _elapsedTimeOnLoad + (int)Time.timeSinceLevelLoad - _timeOfLastLoad;

            for (var i = 0; i < _executableObjects.Length; i++)
            {
                var executeableObject = _executableObjects[i];

                if (executeableObject == null)
                {
                    continue;
                }
                executeableObject.Execute();
            }

            _inputController.Execute();
        }


        private void FixedUpdate()
        {
            _inputController.ExecuteFixed();
        }

        private void ChangeControl()
        {
            _isUsingAlternativeControl = !_isUsingAlternativeControl;
        }

        private void PauseGame()
        {
            _isGamePaused = !_isGamePaused;

            if (_isGamePaused)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }

        private void SaveGame()
        {
            List<InteractiveObject> intObjs = new List<InteractiveObject>();
            foreach (var o in _executableObjects)
                if (o is InteractiveObject)
                    intObjs.Add(o as InteractiveObject);

            GameStatsInfo gameStats = new GameStatsInfo(_scoreTracker.NeededItems, _scoreTracker.Score, _currentElapsedTime);

            _saveDataRepository.Save(_player.GetComponent<PlayerBall>(), intObjs, gameStats);
        }

        private void LoadGame()
        {
            List<InteractiveObject> intObjs = new List<InteractiveObject>();
            foreach (var o in _executableObjects)
                if (o is InteractiveObject)
                    intObjs.Add(o as InteractiveObject);

            GameStatsInfo gameStats = new GameStatsInfo();

            _saveDataRepository.Load(_player.GetComponent<PlayerBall>(), intObjs, ref gameStats);
            RestoreStats(gameStats);
        }

        private void RestoreStats(GameStatsInfo stats)
        {
            _timeOfLastLoad = (int)Time.timeSinceLevelLoad;
            _elapsedTimeOnLoad = stats.GameTime;
            _scoreTracker.RestoreOnLoad(stats);
        }

        private void OnGUI()
        {
            _currentTime = System.TimeSpan.FromSeconds(_currentElapsedTime).ToString();
            GUI.Box(new Rect(0, 0, 270, 90), "");
            GUI.Label(new Rect(10, 5, 240, 20), "Собрано необходимых предметов: " + _scoreTracker.NeededItems + "/" + _necessaryBonusTotalCount);
            GUI.DrawTexture(new Rect(240, 0, 30, 30), _necessaryBonusImage);
            GUI.Label(new Rect(10, 35, 200, 20), "Бонусных очков: " + _scoreTracker.Score);
            GUI.Label(new Rect(10, 65, 200, 20), "Время: " + _currentTime);

            if (_isPlayerAtFinish)
            {
                if (_scoreTracker.NeededItems != _necessaryBonusTotalCount)
                {
                    GUILayout.BeginArea(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 150, 300, 300));
                    GUI.Box(new Rect(0, 0, 300, 300), "Для завершения уровня нужно собрать все");
                    GUI.DrawTexture(new Rect(20, 30, 260, 260), _necessaryBonusImage);
                    GUILayout.EndArea();
                }
                else
                {
                    GUILayout.BeginArea(new Rect(Screen.width / 2 - 125, 25, 250, 150));
                    GUI.Box(new Rect(0, 0, 250, 150), "Уровень успешно пройден");
                    GUI.Label(new Rect(10, 30, 230, 20), "Вы набрали " + _scoreTracker.Score + " бонусных очков");
                    GUI.Label(new Rect(10, 60, 230, 20), "Время прохождения: " + _currentTime);
                    if (GUI.Button(new Rect(10, 90, 230, 20), "Заново"))
                    {
                        RestartGame();
                    }
                    if (GUI.Button(new Rect(10, 120, 230, 20), "Выход"))
                        Application.Quit();
                    GUILayout.EndArea();
                }
            }

            if (_isGamePaused)
            {
                GUILayout.BeginArea(new Rect(Screen.width / 2 - 125, 25, 250, 150));
                GUI.Box(new Rect(0, 0, 250, 100), "Пауза");
                if (GUI.Button(new Rect(10, 30, 230, 20), "Заново"))
                {
                    RestartGame();
                }
                if (GUI.Button(new Rect(10, 60, 230, 20), "Продолжить"))
                {
                    _isGamePaused = false;
                    Time.timeScale = 1;
                }    
                GUILayout.EndArea();
            }

            GUI.Box(new Rect(0, Screen.height - 70, 360, 70), "");
            GUI.Label(new Rect(10, Screen.height - 60, 350, 20), "Для переключения метода управления нажмите TAB");
            string controlMethod;
            if (_isUsingAlternativeControl)
                controlMethod = "Мышь";
            else
                controlMethod = "Клавиатура/Геймпад";
            GUI.Label(new Rect(10, Screen.height - 30, 350, 20), "Текущий метод управления: " + controlMethod);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _player) _isPlayerAtFinish = true;
            if (_scoreTracker.NeededItems == _necessaryBonusTotalCount) Time.timeScale = 0;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == _player) _isPlayerAtFinish = false;
        }

        private void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        private void SetUpGame(Reference reference)
        {
            List<GoodBonus> goodBonus = new List<GoodBonus>();
            List<BadBonus> badBonus = new List<BadBonus>();
            goodBonus.AddRange(CloneObjects(reference.NecessaryBonus, "NecessaryBonusSpawn"));
            goodBonus.AddRange(CloneObjects(reference.ScoreBonus, "ScoreBonusSpawn"));
            goodBonus.AddRange(CloneObjects(reference.SpeedBonus, "SpeedBonusSpawn"));
            goodBonus.AddRange(CloneObjects(reference.InvincibilityBonus, "InvincibilityBonusSpawn"));
            badBonus.AddRange(CloneObjects(reference.BadBonus, "SlowBonusSpawn"));
            
            CloneObjects(reference.SpringTrap, "SpringTrapSpawn"); 
            CloneObjects(reference.SlowFieldTrap, "SlowFieldSpawn"); 

            _bonuses = new BonusHandler(goodBonus.ToArray(), badBonus.ToArray());

            for (int i = 0; i < _bonuses.goodBonusLength; i++)
            {
                _bonuses[i].PlayerInteraction += _cameraController.Shake;
                if (_bonuses[i] is NecessaryBonus)
                    (_bonuses[i] as NecessaryBonus).PlayerInteraction += _scoreTracker.GetNeededItem;
                if (_bonuses[i] is ScoreBonus)
                    (_bonuses[i] as ScoreBonus).GetBonusScore += _scoreTracker.AddScore;
            }
            for (double i = 0; i < _bonuses.badBonusLength; i++)
            {
                _bonuses[i].PlayerInteraction += _cameraController.Shake;
            }
        }

        //Обобщенный метод для клонирования бонусов
        private List<T> CloneObjects<T>(T bonusToClone, string tag) where T : InteractiveObject
        {
            bonusToClone.RegisterPlayer(_player.GetComponent<PlayerBall>());
            GameObject[] existingSpawns = GameObject.FindGameObjectsWithTag(tag);

            if (existingSpawns.Length < 2)
                throw new Exception($"Level must have multiple object spawn points with tag «{tag}» set up");

            List<GameObject> spawns = new List<GameObject>();
            foreach (GameObject spawn in existingSpawns)
                spawns.Add(spawn);

            List<T> bonuses = new List<T>();
            bonuses.Add(bonusToClone);

            for (int i = 0; i < existingSpawns.Length / 2 - 1; i++)
            {
                var clone = bonusToClone.Clone();
                (clone as GameObject).GetComponent<T>().RegisterPlayer(_player.GetComponent<PlayerBall>());
                bonuses.Add((clone as GameObject).GetComponent<T>());
            }

            foreach (T bonus in bonuses)
            {
                Transform position = spawns[UnityEngine.Random.Range(0, spawns.Count)].transform;
                bonus.gameObject.transform.position = position.position;
                bonus.gameObject.transform.rotation = position.rotation;
                spawns.Remove(position.gameObject);
            }

            return bonuses;
        }
    }
}