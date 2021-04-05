using UnityEngine;

namespace BallGame
{
    public class Reference
    {
        private PlayerBall _playerBall;
        private Camera _mainCamera;
        private BadBonus _badBonus;
        private NecessaryBonus _necessaryBonus;
        private ScoreBonus _scoreBonus;
        private SpeedBonus _speedBonus;
        private InvincibilityBonus _invincibilityBonus;
        private SpringTrap _springTrap;
        private SlowFieldTrap _slowFieldTrap;
        private Camera _miniMapCamera;
        private GameObject _radar;

        public PlayerBall PlayerBall
        {
            get
            {
                if (_playerBall == null)
                {
                    var gameObject = Resources.Load<PlayerBall>("Player");
                    _playerBall = Object.Instantiate(gameObject);
                }

                return _playerBall;
            }
        }

        public Camera MainCamera
        {
            get
            {
                if (_mainCamera == null)
                {
                    _mainCamera = Camera.main;
                }
                return _mainCamera;
            }
        }

        public BadBonus BadBonus
        {
            get 
            {
                if (_badBonus == null)
                {
                    var gameObject = Resources.Load<BadBonus>("SlowDown");
                    _badBonus = Object.Instantiate(gameObject);
                }

                return _badBonus;
            }
        }

        public NecessaryBonus NecessaryBonus
        {
            get
            {
                if (_necessaryBonus == null)
                {
                    var gameObject = Resources.Load<NecessaryBonus>("NecessaryBonus");
                    _necessaryBonus = Object.Instantiate(gameObject);
                }

                return _necessaryBonus;
            }
        }

        public ScoreBonus ScoreBonus
        {
            get
            {
                if (_scoreBonus == null)
                {
                    var gameObject = Resources.Load<ScoreBonus>("ScoreBonus");
                    _scoreBonus = Object.Instantiate(gameObject);
                }

                return _scoreBonus;
            }
        }

        public SpeedBonus SpeedBonus
        {
            get
            {
                if (_speedBonus == null)
                {
                    var gameObject = Resources.Load<SpeedBonus>("SpeedUp");
                    _speedBonus = Object.Instantiate(gameObject);
                }

                return _speedBonus;
            }
        }

        public InvincibilityBonus InvincibilityBonus
        {
            get
            {
                if (_invincibilityBonus == null)
                {
                    var gameObject = Resources.Load<InvincibilityBonus>("Invincibility");
                    _invincibilityBonus = Object.Instantiate(gameObject);
                }

                return _invincibilityBonus;
            }
        }

        public SpringTrap SpringTrap
        {
            get
            {
                if (_springTrap == null)
                {
                    var gameObject = Resources.Load<SpringTrap>("SpringTrap");
                    _springTrap = Object.Instantiate(gameObject);
                }

                return _springTrap;
            }
        }

        public SlowFieldTrap SlowFieldTrap
        {
            get
            {
                if (_slowFieldTrap == null)
                {
                    var gameObject = Resources.Load<SlowFieldTrap>("SlowField");
                    _slowFieldTrap = Object.Instantiate(gameObject);
                }

                return _slowFieldTrap;
            }
        }

        public Camera MiniMapCamera
        {
            get
            {
                if (_miniMapCamera == null)
                {
                    _miniMapCamera = GameObject.FindGameObjectWithTag("MiniMapCamera").GetComponent<Camera>();
                }
                return _miniMapCamera;
            }
        }

        public GameObject Radar
        {
            get
            {
                if (_radar == null)
                {
                    _radar = GameObject.FindGameObjectWithTag("Radar");
                }

                return _radar;
            }
        }
    }
}