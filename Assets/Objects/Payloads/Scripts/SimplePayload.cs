using UnityEngine;

namespace Payloads
{
    public class SimplePayload : Payload
    {
        [SerializeField] private Rigidbody _rigidbody;

        public override bool IsManualControl
        {
            get => _rigidbody.isKinematic;
            set => _rigidbody.isKinematic = value;
        }
    }
}