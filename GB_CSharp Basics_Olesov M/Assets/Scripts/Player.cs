using System;
using System.Collections;
using UnityEngine;

namespace BallGame
{
    public class Player : MonoBehaviour, IFlickerable
    {
        [SerializeField] private float _defaultSpeed = 3.0f;

        protected float _speed;

        private float _speedUpModifier = 0.0f;
        private float _slowDownModifier = 0.0f;
        private float _speedUpDuration;
        private float _slowDownDuration;

        private bool _isInvincible;
        private float _invincibilityDuration;

        protected Rigidbody _rigidbody;
        private Material _material;

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            _rigidbody = GetComponent<Rigidbody>();
        }

        protected virtual void Move(bool _isUsingAlternativeControl)
        {
            float moveHorizontal;
            float moveVertical;
            if (_isUsingAlternativeControl)
            {
                moveHorizontal = Input.GetAxis("Mouse X");
                moveVertical = Input.GetAxis("Mouse Y");
            }
            else
            {
                moveHorizontal = Input.GetAxis("Horizontal");
                moveVertical = Input.GetAxis("Vertical");
            }

            if (_speedUpModifier > 0 && _speedUpDuration <= 0)
                _speedUpModifier = 0;
            if (_slowDownModifier < 0 && _slowDownDuration <= 0)
                _slowDownModifier = 0;

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            _speed = _defaultSpeed * (1 + _speedUpModifier + _slowDownModifier);
            _rigidbody.AddForce(movement * _speed);
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

        public bool CheckInvincibility() => _isInvincible;

        protected void OnGUI()
        {
            if (_speedUpDuration > 0)
            {
                GUI.Box(new Rect(Screen.width - 240, Screen.height - 60, 240, 30), "");
                GUI.Label(new Rect(Screen.width - 235, Screen.height - 55, 230, 20), "ƒлительность ускорени€: " + _speedUpDuration.ToString("0.00"));
            }
            if (_slowDownDuration > 0)
            {
                GUI.Box(new Rect(Screen.width - 240, Screen.height - 30, 240, 30), "");
                GUI.Label(new Rect(Screen.width - 235, Screen.height - 25, 230, 20), "ƒлительность замедлени€: " + _slowDownDuration.ToString("0.00"));
            }
            if (_invincibilityDuration > 0)
            {
                GUI.Box(new Rect(Screen.width - 240, Screen.height - 90, 240, 30), "");
                GUI.Label(new Rect(Screen.width - 235, Screen.height - 85, 230, 20), "ƒлительность неу€звимости: " + _invincibilityDuration.ToString("0.00"));
                Flicker();
            }
        }

        public void Flicker()
        {
            _material.color = new Color(_material.color.r, _material.color.g,
                                        _material.color.b, Mathf.PingPong(Time.time, 1.0f));
        }

     }
}