using UnityEngine;

namespace ForkLift.Panels
{
    public class StatusPanel : MonoBehaviour
    {
        [SerializeField] private EngineStatus _engineStatus;
        [SerializeField] private FuelStatus _fuelStatus;

        public EngineStatus EngineStatus => _engineStatus;
        public FuelStatus FuelStatus => _fuelStatus;
    }
}