using System;
using System.Collections;
using UnityEngine;

namespace BallGame
{
    public sealed class PlayerBall : PlayerBase, IFlickerable
    {
        private GameObject _ground;
        private bool _isGrounded;

        private float _scoreMultiplier = 0.0f;
        private float _scoreMultiplierCountDown = 1.0f;
        private float _currentSpeed;

        private float _speedUpModifier = 0.0f;
        private float _slowDownModifier = 0.0f;
        private float _speedUpDuration;
        private float _slowDownDuration;

        private bool _isInvincible;
        private float _invincibilityDuration;

        public delegate void BonusScore(int score);
        public event BonusScore GetBonusScore;

        private void Start()
        {
            _ground = GameObject.FindGameObjectWithTag("Ground");
        }

        public override void Move(float x, float y, float z)
        {
            if (_isGrounded)
            {
                if (_speedUpModifier > 0 && _speedUpDuration <= 0)
                    _speedUpModifier = 0;
                if (_slowDownModifier < 0 && _slowDownDuration <= 0)
                    _slowDownModifier = 0;

                Vector3 movement = new Vector3(x, y, z);

                _speed = _defaultSpeed * (1 + _speedUpModifier + _slowDownModifier);
                _rigidbody.AddForce(movement * _speed);
            }

            Scoring();
        }

        private void Scoring()
        {
            _currentSpeed = _rigidbody.velocity.magnitude;
            if (_scoreMultiplierCountDown > 0)
                _scoreMultiplierCountDown -= Time.deltaTime;
            if (_scoreMultiplierCountDown <= 0)
            {
                if (_rigidbody.velocity.magnitude > 1)
                    _scoreMultiplier += 0.1f;
                GetBonusScore?.Invoke((int)(_scoreMultiplier * _currentSpeed));
                _scoreMultiplierCountDown = 1.0f;
            }
        }

        private void OnGUI()
        {
            GUI.Box(new Rect(Screen.width - 170, 0, 170, 30), "");
            GUI.Label(new Rect(Screen.width - 160, 0, 160, 20), "Множитель очков: " + _scoreMultiplier.ToString("0.0"));


            if (_speedUpDuration > 0)
            {
                GUI.Box(new Rect(Screen.width - 240, Screen.height - 60, 240, 30), "");
                GUI.Label(new Rect(Screen.width - 235, Screen.height - 55, 230, 20), "Длительность ускорения: " + _speedUpDuration.ToString("0.00"));
            }
            if (_slowDownDuration > 0)
            {
                GUI.Box(new Rect(Screen.width - 240, Screen.height - 30, 240, 30), "");
                GUI.Label(new Rect(Screen.width - 235, Screen.height - 25, 230, 20), "Длительность замедления: " + _slowDownDuration.ToString("0.00"));
            }
            if (_invincibilityDuration > 0)
            {
                GUI.Box(new Rect(Screen.width - 240, Screen.height - 90, 240, 30), "");
                GUI.Label(new Rect(Screen.width - 235, Screen.height - 85, 230, 20), "Длительность неуязвимости: " + _invincibilityDuration.ToString("0.00"));
                Flicker();
            }
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

        public void ModifySpeed(float modifier, float duration)
        {
            if (modifier > 0)
            {
                if (_speedUpModifier < modifier)
                    _speedUpModifier = modifier;
                _speedUpDuration += duration;
            }
            if (modifier < 0)
            {
                if (_slowDownModifier > modifier)
                    _slowDownModifier = modifier;
                _slowDownDuration += duration;
            }
            StopCoroutine("CancelBonus");
            StartCoroutine("CancelBonus");
        }

        IEnumerator CancelBonus()
        {
            while (_speedUpDuration > 0 || _slowDownDuration > 0)
            {
                if (_speedUpDuration > 0)
                    _speedUpDuration -= Time.deltaTime;

                if (_slowDownDuration > 0)
                    _slowDownDuration -= Time.deltaTime;
                yield return null;
            }
            StopCoroutine("CancelBonus");

        }

        public void GetInvincibility(float duration)
        {
            _invincibilityDuration += duration;
            _isInvincible = true;
            StopCoroutine("CancelInvincibility");
            StartCoroutine("CancelInvincibility");
        }

        IEnumerator CancelInvincibility()
        {
            while (_invincibilityDuration > 0)
            {
                _invincibilityDuration -= Time.deltaTime;
                yield return null;
            }
            _isInvincible = false;
            _material.color = new Color(_material.color.r, _material.color.g,
                                        _material.color.b, 1.0f);
            StopCoroutine("CancelInvincibility");
        }

        public bool IsInvincible => _isInvincible;

        public void Flicker()
        {
            _material.color = new Color(_material.color.r, _material.color.g,
                                        _material.color.b, Mathf.PingPong(Time.time, 1.0f));
        }

        public BonusInfo CurrentBonusInfo
        {
            get
            {
                return new BonusInfo(
                    _scoreMultiplier,
                    _scoreMultiplierCountDown,
                    _currentSpeed,
                    _speedUpModifier,
                    _slowDownModifier,
                    _speedUpDuration,
                    _slowDownDuration,
                    _isInvincible,
                    _invincibilityDuration
                    );
            }

            set
            {
                _scoreMultiplier = value.scoreMultiplier;
                _scoreMultiplierCountDown = value.scoreMultiplierCountDown;
                _currentSpeed = value.currentSpeed;
                _speedUpModifier = value.speedUpModifier;
                _slowDownModifier = value.slowDownModifier;
                _speedUpDuration = value.speedUpDuration;
                _slowDownDuration = value.slowDownDuration;
                _isInvincible = value.isInvincible;
                _invincibilityDuration = value.invincibilityDuration;
                StartCoroutine("CancelBonus");
                StartCoroutine("CancelInvincibility");
            }
        }
    }
}
