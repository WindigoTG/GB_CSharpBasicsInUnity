using UnityEngine;

namespace BallGame
{
    public sealed class InputController : IExecuteable
    {
        private readonly PlayerBase _playerBase;
        private bool _isUsingAlternativeControl;

        public InputController(PlayerBase player)
        {
            _playerBase = player;
        }

        public void Execute()
        {
            if (_isUsingAlternativeControl)
                _playerBase.Move(Input.GetAxis("Mouse X"), 0.0f, Input.GetAxis("Mouse Y"));
            else
                _playerBase.Move(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        }

        public void ChangeControl()
        {
            _isUsingAlternativeControl = !_isUsingAlternativeControl;
        }
    }
}