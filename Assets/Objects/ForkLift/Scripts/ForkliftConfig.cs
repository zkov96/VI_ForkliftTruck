using UnityEngine;

namespace ForkLift
{
    [CreateAssetMenu(menuName = "Configs/Forklift")]
    public class ForkliftConfig : ScriptableObject
    {
        public float WheelMotorTorque;
        public float WheelBrakeTorque;

        public float RotateSpeed;
        public float ReviveRotateSpeed;
        
        public float ForkSpeed;

        public float FuelConsumption;
        public float FuelCapacity;
        public AnimationCurve SpeedByFuelPercentageCurve;

        public Vector2 CameraRotationSpeed;
        public Vector2 CameraHorizontalMinMax;
        public Vector2 CameraVerticalMinMax;
        public float CameraSmoothness;
        public float CameraZoomSpeed;
    }
}