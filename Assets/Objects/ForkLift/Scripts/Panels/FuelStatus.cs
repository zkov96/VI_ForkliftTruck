using UnityEngine;

namespace ForkLift.Panels
{
    public class FuelStatus : MonoBehaviour
    {
        [SerializeField] private Transform _fuelStatusTransform;

        [SerializeField] private Vector2 _rotationMinMax;

        public float Fuel
        {
            get => Mathf.InverseLerp(_rotationMinMax.x, _rotationMinMax.y, _fuelStatusTransform.localEulerAngles.z);
            set => _fuelStatusTransform.localRotation =
                Quaternion.Euler(0, 0, Mathf.Lerp(_rotationMinMax.x, _rotationMinMax.y, value));
        }

        private void Start()
        {
            Fuel = 0;
        }
    }
}