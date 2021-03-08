using UnityEngine;

namespace BallGame
{
    public sealed class PlayerBall : Player
    {
        private bool _isUsingAlternativeControl;

        [SerializeField] private GameObject _ground;
        private bool _isGrounded;

        private float _scoreMultiplier = 0.0f;
        private float _scoreMultiplierCountDown = 1.0f;
        private float _currentSpeed;

        private ScoreTracker _scoreTracker;

        private void Start()
        {
            _scoreTracker = FindObjectOfType<ScoreTracker>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                _isUsingAlternativeControl = !_isUsingAlternativeControl;
            Scoring();
        }

        private void FixedUpdate()
        {
            if (_isGrounded)
                Move(_isUsingAlternativeControl);
        }

        private void Scoring()
        {
            _currentSpeed = _rigidbody.velocity.magnitude;
            if (_scoreMultiplierCountDown > 0)
                _scoreMultiplierCountDown -= Time.deltaTime;
            if (_scoreMultiplierCountDown <= 0)
            {
                if (!Mathf.Approximately(_rigidbody.velocity.magnitude, 0))
                    _scoreMultiplier += 0.1f;
                _scoreTracker.AddScore((int)(_scoreMultiplier * _currentSpeed));
                _scoreMultiplierCountDown = 1.0f;
            }
        }

        private new void OnGUI()
        {
            base.OnGUI();
            GUI.Box(new Rect(0, Screen.height-70, 360, 70), "");
            GUI.Label(new Rect(10, Screen.height-60, 350, 20), "Для переключения метода управления нажмите TAB");
            string controlMethod;
            if (_isUsingAlternativeControl)
                controlMethod = "Мышь";
            else
                controlMethod = "Клавиатура/Геймпад";
            GUI.Label(new Rect(10, Screen.height-30, 350, 20), "Текущий метод управления: " + controlMethod);

            GUI.Box(new Rect(Screen.width - 170, 0, 170, 30), "");
            GUI.Label(new Rect(Screen.width - 160, 0, 160, 20), "Множитель очков: " + _scoreMultiplier.ToString("0.0"));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject == _ground)
                _isGrounded = true;
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
                _scoreMultiplier = 0.0f;
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject == _ground)
                _isGrounded = false;
        }
    }
}
