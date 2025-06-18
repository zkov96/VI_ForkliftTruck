using UnityEngine;

namespace Payloads.Pallete.Scripts
{
    public class Pallete : Payload
    {
        [SerializeField] private Rigidbody[] _rigidbodys;
        
        private bool _isManualControl;
        public override bool IsManualControl
        {
            get => _isManualControl;
            set
            {
                _isManualControl = value;
                foreach (var rigidbody in _rigidbodys)
                {
                    rigidbody.isKinematic = value;
                }
            }
        }
    }
}