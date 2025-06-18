using UnityEngine;
using UnityEngine.InputSystem;

namespace ForkLift
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _normalTransform;
        [SerializeField] private Transform _zoomTransform;

        [SerializeField] private ForkliftConfig _config;

        private InputAction _cameraAction;
        private InputAction _cameraZoomAction;

        private Vector3 _rotation;
        private float _zoomProgress;

        private void Start()
        {
            _cameraAction = InputSystem.actions.FindAction("Look");
            _cameraZoomAction = InputSystem.actions.FindAction("LookZoom");
        }

        private void Update()
        {
            var cameraValues = _cameraAction.ReadValue<Vector2>();

            _rotation = new Vector3(
                Mathf.Clamp(
                    _rotation.x - cameraValues.y * _config.CameraRotationSpeed.y * Time.deltaTime,
                    _config.CameraVerticalMinMax.x,
                    _config.CameraVerticalMinMax.y
                ),
                Mathf.Clamp(
                    _rotation.y + cameraValues.x * _config.CameraRotationSpeed.x * Time.deltaTime,
                    _config.CameraHorizontalMinMax.x,
                    _config.CameraHorizontalMinMax.y
                ),
                0
            );

            _normalTransform.localRotation = Quaternion.Lerp(
                _cameraTransform.localRotation,
                Quaternion.Euler(_rotation),
                _config.CameraSmoothness
            );

            var cameraZoom = _cameraZoomAction.IsPressed() ? 1 : -1;

            _zoomProgress = Mathf.Clamp01(_zoomProgress + cameraZoom * _config.CameraZoomSpeed * Time.deltaTime);

            _cameraTransform.Lerp(_normalTransform, _zoomTransform, _zoomProgress);
        }
    }
}