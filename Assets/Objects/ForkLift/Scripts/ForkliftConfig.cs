using UnityEngine;

namespace ForkLift
{
    [CreateAssetMenu(menuName = "Configs/Forklift")]
    public class ForkliftConfig : ScriptableObject
    {
        public float WheelMotorTorque;
        public float WheelBrakeTorque;
        public float MaxSpeed;

        public float RotateSpeed;
        public float ReviveRotateSpeed;
        
        public float MovingSpeed;
        public float ForkSpeed;
    }
}