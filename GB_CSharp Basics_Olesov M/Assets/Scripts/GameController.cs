using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BallGame
{
    public class GameController : MonoBehaviour
    {
        private BonusHandler _bonuses;
        private ScoreTracker _scoreTracker;
        private string _time;
        private GameObject _player;
        private bool _isPlayerAtFinish;
        private int _necessaryBonusTotalCount;

        [SerializeField] Texture _necessaryBonusImage;

        private void Awake()
        {
            Time.timeScale = 1;
            _player = GameObject.FindGameObjectWithTag("Player");
            SetUpGame();
            _scoreTracker = FindObjectOfType<ScoreTracker>();
            NecessaryBonus[] necessaryBonus = FindObjectsOfType<NecessaryBonus>();
            _necessaryBonusTotalCount = necessaryBonus.Length;
        }

        void Update()
        {
            for (int i = 0; i < _bonuses.goodBonusLength; i++)
            {
                if (_bonuses[i] == null)
                {
                    continue;
                }

                if (_bonuses[i] is ILevitateable levitation)
                {
                    levitation.Levitate();
                }
                if (_bonuses[i] is IFlickerable flicker)
                {
                    flicker.Flicker();
                }
                if (_bonuses[i] is IRotatable rotation)
                {
                    rotation.Rotation();
                }
            }

            for (double i = 0; i < _bonuses.badBonusLength; i++)
            {
                if (_bonuses[i] == null)
                {
                    continue;
                }

                if (_bonuses[i] is ILevitateable levitation)
                {
                    levitation.Levitate();
                }
                if (_bonuses[i] is IRotatable rotation)
                {
                    rotation.Rotation();
                }
            }
        }

        private void OnGUI()
        {
            _time = System.TimeSpan.FromSeconds((int)Time.timeSinceLevelLoad).ToString();
            GUI.Box(new Rect(0, 0, 270, 90), "");
            GUI.Label(new Rect(10, 5, 240, 20), "Собрано необходимых предметов: " + _scoreTracker.NeededItems + "/" + _necessaryBonusTotalCount);
            GUI.DrawTexture(new Rect(240, 0, 30, 30), _necessaryBonusImage);
            GUI.Label(new Rect(10, 35, 200, 20), "Бонусных очков: " + _scoreTracker.Score);
            GUI.Label(new Rect(10, 65, 200, 20), "Время: " + _time);

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
                    Time.timeScale = 0;
                    GUILayout.BeginArea(new Rect(Screen.width / 2 - 125, 25, 250, 150));
                    GUI.Box(new Rect(0, 0, 250, 150), "Уровень успешно пройден");
                    GUI.Label(new Rect(10, 30, 230, 20), "Вы набрали " + _scoreTracker.Score + " бонусных очков");
                    GUI.Label(new Rect(10, 60, 230, 20), "Время прохождения: " + _time);
                    if (GUI.Button(new Rect(10, 90, 230, 20), "Заново"))
                    {
                        Time.timeScale = 1;
                        SceneManager.LoadScene(0);
                    }
                    if (GUI.Button(new Rect(10, 120, 230, 20), "Выход"))
                        Application.Quit();
                    GUILayout.EndArea();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _player) _isPlayerAtFinish = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == _player) _isPlayerAtFinish = false;
        }

        private void SetUpGame()
        {
            List<GoodBonus> goodBonus = new List<GoodBonus>();
            List<BadBonus> badBonus = new List<BadBonus>();
            goodBonus.AddRange(CloneObjects(FindObjectOfType<NecessaryBonus>(), "NecessaryBonusSpawn"));
            goodBonus.AddRange(CloneObjects(FindObjectOfType<ScoreBonus>(), "ScoreBonusSpawn"));
            goodBonus.AddRange(CloneObjects(FindObjectOfType<SpeedBonus>(), "SpeedBonusSpawn"));
            goodBonus.AddRange(CloneObjects(FindObjectOfType<InvincibilityBonus>(), "InvincibilityBonusSpawn"));
            badBonus.AddRange(CloneObjects(FindObjectOfType<BadBonus>(), "SlowBonusSpawn"));
            
            CloneObjects(FindObjectOfType<SpringTrap>(), "SpringTrapSpawn"); 
            CloneObjects(FindObjectOfType<SlowFieldTrap>(), "SlowFieldSpawn"); 

            _bonuses = new BonusHandler(goodBonus.ToArray(), badBonus.ToArray());
        }

        //Обобщенный метод для клонирования бонусов
        private List<T> CloneObjects<T>(T bonusToClone, string tag) where T : InteractiveObject
        {
            GameObject[] existingSpawns = GameObject.FindGameObjectsWithTag(tag);

            List<GameObject> spawns = new List<GameObject>();
            foreach (GameObject spawn in existingSpawns)
                spawns.Add(spawn);

            List<T> bonuses = new List<T>();
            bonuses.Add(bonusToClone);

            for (int i = 0; i < existingSpawns.Length / 2 - 1; i++)
            {
                var clone = bonusToClone.Clone();
                bonuses.Add((clone as GameObject).GetComponent<T>());
            }

            foreach (T bonus in bonuses)
            {
                Transform position = spawns[Random.Range(0, spawns.Count)].transform;
                bonus.gameObject.transform.position = position.position;
                bonus.gameObject.transform.rotation = position.rotation;
                spawns.Remove(position.gameObject);
            }

            return bonuses;
        }
    }
}