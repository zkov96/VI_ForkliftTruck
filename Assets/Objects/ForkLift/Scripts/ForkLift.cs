using UnityEngine;
using UnityEngine.InputSystem;

namespace ForkLift
{
    [RequireComponent(typeof(Rigidbody))]
    public class ForkLift : MonoBehaviour
    {
        [SerializeField] private Wheel[] _wheels;
        [SerializeField] private ControlWheel[] _controlWheels;

        [SerializeField] private Fork _fork;

        [SerializeField] private ForkliftConfig _config;

        private float _wheelTorque;

        private InputAction _engineAction;
        private InputAction _gasAction;
        private InputAction _rotateAction;
        private InputAction _forkAction;

        private bool _isEngineStarted;
        private bool _isEngineButtonPressed;

        /// <summary>
        /// 0-1
        /// </summary>
        private float _fuel;
        private float _fuelConsumption;

        private void Awake()
        {
            _wheelTorque = _config.WheelMotorTorque / _wheels.Length;
            _fuelConsumption = _config.FuelConsumption / _config.FuelCapacity;
            _fuel = 1;
        }

        private void Start()
        {
            _engineAction = InputSystem.actions.FindAction("Engine");
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
            if (!_isEngineButtonPressed && _engineAction.IsPressed())
            {
                _isEngineButtonPressed = true;
                _isEngineStarted = _fuel > 0;
            }
            else if (_isEngineButtonPressed && !_engineAction.IsPressed())
            {
                _isEngineButtonPressed = false;
            }
            
            if (_isEngineStarted)
            {
                var gasDirection = _gasAction.ReadValue<float>();
                var isBrake = !_gasAction.IsPressed();

                var forkDirection = _forkAction.ReadValue<float>();

                // float currentForwardSpeed = Vector3.Dot(transform.forward, _rigidbody.linearVelocity);
                // float speedFactor = Mathf.InverseLerp(-_config.MaxSpeed, _config.MaxSpeed, currentForwardSpeed);
                // float currentMotorTorque = Mathf.Lerp(-_wheelTorque, _wheelTorque, speedFactor);
                foreach (var wheel in _wheels)
                {
                    wheel.MotorTorque = _wheelTorque * gasDirection * _config.SpeedByFuelPercentageCurve.Evaluate(_fuel);
                    wheel.BrakeTorque = isBrake ? _config.WheelBrakeTorque : 0;
                }

                _fork.Speed = _config.ForkSpeed * forkDirection;
                
                _fuel -= _fuelConsumption * Time.deltaTime;
            }
            else
            {
                foreach (var wheel in _wheels)
                {
                    wheel.MotorTorque = 0;
                    wheel.BrakeTorque = _config.WheelBrakeTorque;
                }
            }
            
            var rotateDirection = -_rotateAction.ReadValue<float>();
            foreach (var controlWheel in _controlWheels)
            {
                controlWheel.RotationSpeed = _config.RotateSpeed * rotateDirection;
            }
            
            if (_fuel <= 0)
            {
                _fuel = 0;
                _isEngineStarted = false;
            }
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