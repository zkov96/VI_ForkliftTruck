using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Forklift
{
    [RequireComponent(typeof(Rigidbody))]
    public class ForkLift : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Wheel[] _wheels;
        [SerializeField] private ControlWheel[] _controlWheels;

        [SerializeField] private Fork _fork;

        [SerializeField] private ForkliftConfig _config;

        private Rigidbody _rigidbody;
        private float _wheelTorque;

        private InputAction _gasAction;
        private InputAction _rotateAction;
        private InputAction _forkAction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _wheelTorque = _config.WheelMotorTorque / _wheels.Length;
        }

        private void Start()
        {
            _gasAction = InputSystem.actions.FindAction("Gas");
            _rotateAction = InputSystem.actions.FindAction("Rotate");
            _forkAction = InputSystem.actions.FindAction("Fork");
            
            foreach (var controlWheel in _controlWheels)
            {
                controlWheel.ReviveRotateSpeed = _config.ReviveRotateSpeed;
            }
        }

        private void Update()
        {
            var gasDirection = _gasAction.ReadValue<float>();
            var isBrake = !_gasAction.IsPressed();
            
            var rotateDirection = _rotateAction.ReadValue<float>();
            
            var forkDirection = _forkAction.ReadValue<float>();

            // float currentForwardSpeed = Vector3.Dot(transform.forward, _rigidbody.linearVelocity);
            // float speedFactor = Mathf.InverseLerp(-_config.MaxSpeed, _config.MaxSpeed, currentForwardSpeed);
            // float currentMotorTorque = Mathf.Lerp(-_wheelTorque, _wheelTorque, speedFactor);
            foreach (var wheel in _wheels)
            {
                wheel.MotorTorque = _wheelTorque * gasDirection;
                wheel.BrakeTorque = isBrake ? _config.WheelBrakeTorque : 0;
            }
            
            foreach (var controlWheel in _controlWheels)
            {
                controlWheel.RotationSpeed = _config.RotateSpeed * rotateDirection;
            }

            _fork.Speed = _config.ForkSpeed * forkDirection;
        }

        private void OnGUI()
        {
            foreach (var wheel in _wheels)
            {
                GUILayout.Box($"{wheel.name}: M={wheel.MotorTorque} B={wheel.BrakeTorque}");
            }

            GUILayout.Box($"_fork.Speed = {_fork.Speed}");
        }
    }
}