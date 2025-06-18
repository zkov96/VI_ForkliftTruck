using UnityEngine;

namespace Payloads
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class Payload : MonoBehaviour
    {
        public virtual bool IsManualControl { get; set; }
    }
}