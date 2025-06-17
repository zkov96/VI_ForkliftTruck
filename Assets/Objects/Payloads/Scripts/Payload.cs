using UnityEngine;

namespace Objects.Payloads
{
    [RequireComponent(typeof(BoxCollider))]
    public class Payload : MonoBehaviour
    {
        public virtual bool IsManualControl { get; set; }
    }
}