using System.Linq;
using UnityEngine;
using Zenject;

namespace Payloads.Pallete.Scripts
{
    public class Pallete : PayloadContainer
    {
        [SerializeField] private Rigidbody _rigidbody;

        public override bool IsManualControl
        {
            get => base.IsManualControl;
            set
            {
                base.IsManualControl = value;
                // ((Component)_rigidbody). = value;
                _rigidbody.isKinematic = value;
            }
        }

        protected override Vector3 NewPayloadPosition(int index)
        {
            return new Vector3(
                -0.625f + 0.25f * (index % 12 / 2),
                0.1f * (index / 12),
                0.175f * (index % 2 == 0 ? 1 : -1)
            );
        }
    }
}