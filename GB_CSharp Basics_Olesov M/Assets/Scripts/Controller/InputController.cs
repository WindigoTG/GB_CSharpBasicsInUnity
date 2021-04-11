using UnityEngine;

namespace BallGame
{
    public sealed class InputController : IExecuteable, IExecuteableFixed
    {
        private readonly PlayerBase _playerBase;
        private bool _isUsingAlternativeControl;

        private readonly KeyCode _savePlayer = KeyCode.C;
        private readonly KeyCode _loadPlayer = KeyCode.V;

        public delegate void ObserveInput();
        public event ObserveInput ChangeControlMethod;
        public event ObserveInput PauseGame;
        public event ObserveInput SaveGame;
        public event ObserveInput LoadGame;

        public InputController(PlayerBase player)
        {
            _playerBase = player;
        }

        public void Execute()
        {
            if (Input.GetKeyDown(_savePlayer))
            {
                SaveGame?.Invoke();
            }
            if (Input.GetKeyDown(_loadPlayer))
            {
                LoadGame?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _isUsingAlternativeControl = !_isUsingAlternativeControl;
                ChangeControlMethod?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame?.Invoke();
            }
        }

        public void ExecuteFixed()
        {
            if (_isUsingAlternativeControl)
                _playerBase.Move(Input.GetAxis("Mouse X"), 0.0f, Input.GetAxis("Mouse Y"));
            else
                _playerBase.Move(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        }
    }
}