using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Payloads
{
    public abstract class PayloadContainer : Payload
    {
        [SerializeField] protected Transform _innerPayloadTransform;
        [SerializeField] private Payload _innerPayload;

        [SerializeField] private int _payloadCount;
        [Inject] protected PayloadManager _payloadManager { get; set; }

        protected Payload[] _capturedPayloads = Array.Empty<Payload>();
        
        public override bool IsManualControl
        {
            get => base.IsManualControl;
            set
            {
                foreach (var capturedPayload in _capturedPayloads)
                {
                    capturedPayload.IsManualControl = value;
                }
            }
        }

        public void FreeInnerPayload()
        {
            foreach (var capturedPayload in _capturedPayloads)
            {
                capturedPayload.IsManualControl = false;
                _payloadManager.FreePayload(capturedPayload);
            }

            _capturedPayloads = Array.Empty<Payload>();
        }

        private void Awake()
        {
            OnAwake();
        }
        
        protected virtual void OnAwake()
        {
            if (_innerPayload != null)
            {
                _capturedPayloads = Enumerable.Range(0, _payloadCount)
                    .Select(index =>
                    {
                        var payload = _payloadManager.AllocatePayload(_innerPayload);
                        _payloadManager.CapturePayload(payload, _innerPayloadTransform);
                        payload.transform.localPosition = NewPayloadPosition(index);
                        return payload;
                    })
                    .ToArray();
            }
        }

        protected abstract Vector3 NewPayloadPosition(int index);
    }
}