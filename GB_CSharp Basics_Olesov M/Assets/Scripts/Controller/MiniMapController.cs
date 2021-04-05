using UnityEngine;

namespace BallGame
{
    public sealed class MiniMapController : IExecuteable
    {
        private Transform _player;
        private Transform _camera;

        public MiniMapController(Transform player, Transform camera)
        {
            _player = player;
            _camera = camera;
            _camera.rotation = Quaternion.Euler(90.0f, 0, 0);
            _camera.position = _player.position + new Vector3(0, 10.0f, 0);

            var rt = Resources.Load<RenderTexture>("MiniMap/MiniMapTexture");

            _camera.GetComponent<Camera>().targetTexture = rt;
        }

        public void Execute()
        {
            var newPosition = _player.position;
            newPosition.y = _camera.position.y;
            _camera.position = newPosition;
        }
    }
}