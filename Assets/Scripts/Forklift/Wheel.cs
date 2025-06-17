using UnityEngine;

namespace Forklift
{
    // [RequireComponent(typeof(WheelCollider))]
    public class Wheel : MonoBehaviour
    {
        [SerializeField] protected WheelCollider _wheelCollider;
        [SerializeField] protected Transform _wheelModel;

        public float MotorTorque
        {
            get => _wheelCollider.motorTorque;
            set => _wheelCollider.motorTorque = value;
        }

        public float BrakeTorque
        {
            get => _wheelCollider.brakeTorque;
            set => _wheelCollider.brakeTorque = value;
        }

        // private void Awake()
        // {
        //     _wheelCollider = GetComponent<WheelCollider>();
        // }

        private void Update()
        {
            OnUpdate();
        }
        
        protected virtual void OnUpdate()
        {
            _wheelCollider.GetWorldPose(out var colliderPosition, out var colliderRotation);
            _wheelModel.position = colliderPosition;
            _wheelModel.rotation = colliderRotation;
        }
    }
}