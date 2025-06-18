using UnityEngine;
using Object = UnityEngine.Object;

namespace Payloads
{
    public class PayloadManager
    {
        private Transform _payloadLifetimeHeap;
        private Transform _payloadHeap;

        private PayloadManager()
        {
            _payloadHeap = new GameObject("Payload Heap").transform;
            _payloadLifetimeHeap = new GameObject("Payload Lifetime Heap").transform;
        }

        public T AllocatePayload<T>(T prefab)
            where T : Payload
        {
            return Object.Instantiate(prefab, _payloadLifetimeHeap);
        }

        public void DestroyPayload(Payload payload)
        {
            payload.gameObject.SetActive(false);
            payload.transform.SetParent(_payloadHeap);
        }

        public void CapturePayload(Payload payload, Transform parentPivot, bool worldPositionStays = true)
        {
            payload.transform.SetParent(parentPivot, worldPositionStays);
        }

        public void FreePayload(Payload payload)
        {
            payload.transform.SetParent(_payloadLifetimeHeap);
        }
    }
}