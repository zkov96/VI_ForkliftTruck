using UnityEngine;

namespace Forklift
{
    // [RequireComponent(typeof(WheelCollider))]
    public class ControlWheel : Wheel
    {
        [SerializeField] private Transform _rotatePivot;
        [SerializeField] private float _minRotation;
        [SerializeField] private float _maxRotation;
        
        public float RotationSpeed { get; set; }
        public float ReviveRotateSpeed { get; set; }

        private Quaternion _rotateNormalRotation;
        // private float _currentRotation;
        

        private void Awake()
        {
            _rotateNormalRotation = _rotatePivot.localRotation;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            if (RotationSpeed != 0)
            {
                _wheelCollider.steerAngle = Mathf.Clamp(
                    _wheelCollider.steerAngle + RotationSpeed * Time.deltaTime,
                    _minRotation, _maxRotation
                );
            }
            else
            {
                _wheelCollider.steerAngle = Mathf.Sign(_wheelCollider.steerAngle) * Mathf.Clamp(
                    Mathf.Abs(_wheelCollider.steerAngle) - ReviveRotateSpeed * Time.deltaTime,
                    0, _maxRotation
                );
            }

            _rotatePivot.localRotation = _rotateNormalRotation * Quaternion.Euler(0, _wheelCollider.steerAngle, 0);
        }
    }
}