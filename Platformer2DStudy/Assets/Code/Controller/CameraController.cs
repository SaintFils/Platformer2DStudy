using UnityEngine;

namespace Code.Controller
{
    public class CameraController
    {
        private float _xAxis;
        private float _yAxis;

        private float _offsetX;
        private float _offsetY;

        private int _camSpeed = 300;

        private Transform _playerTransform;
        private Transform _cameraTransform;

        public CameraController(Transform player, Transform camera)
        {
            _playerTransform = player;
            _cameraTransform = camera;
        }

        public void Tick()
        {
            _yAxis = _playerTransform.transform.position.y;
            _xAxis = _playerTransform.transform.position.x;

            _cameraTransform.transform.position = Vector3.Lerp(_cameraTransform.position,
                new Vector3(_xAxis + _offsetX, _yAxis + _offsetY, _cameraTransform.position.z),
                Time.deltaTime * _camSpeed);
        }
    }
}
