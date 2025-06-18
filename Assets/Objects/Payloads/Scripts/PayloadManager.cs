using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Payloads
{
    public class PayloadManager
    {
        [Inject] private DiContainer _diContainer;
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
            return _diContainer.InstantiatePrefabForComponent<T>(prefab, _payloadLifetimeHeap);
        }

        public void DestroyPayload(Payload payload)
        {
            // payload.gameObject.SetActive(false);
            // payload.transform.SetParent(_payloadHeap);
            Object.Destroy(payload.gameObject);
        }

        public Payload CapturePayload(Payload payload, Transform parentPivot, bool worldPositionStays = true)
        {
            payload.transform.SetParent(parentPivot, worldPositionStays);
            return payload;
        }

        public void FreePayload(Payload payload)
        {
            payload.transform.SetParent(_payloadLifetimeHeap);
        }
    }
}