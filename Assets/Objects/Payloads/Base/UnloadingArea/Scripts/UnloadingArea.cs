using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Objects.Payloads.Base.UnloadingArea
{
    [RequireComponent(typeof(BoxCollider))]
    public class UnloadingArea : MonoBehaviour
    {
        [Inject] private PayloadManager _payloadManager { get; set; }

        [SerializeField] private float _captureTime;
        [SerializeField] private Transform _capturePivot;
        [SerializeField] private Transform _targetOut;

        private HashSet<Payload> _capturedPayloads = new();

        private float _captureStartTime = -1;

        private void OnTriggerEnter(Collider other)
        {
            var payload = other.gameObject.GetComponent<Payload>();
            if (payload != null)
            {
                if (!payload.IsManualControl)
                {
                    _capturedPayloads.Add(payload);
                }

                _captureStartTime = Time.time;
            }
        }

        private void Update()
        {
            if (
                _captureStartTime > 0
                && Time.time - _captureStartTime > _captureTime
                && _capturedPayloads.Count > 0
            )
            {
                StartDespawning(_capturedPayloads);
                _capturedPayloads.Clear();
            }
        }

        private void StartDespawning(IEnumerable<Payload> payloads)
        {
            foreach (var payload in payloads)
            {
                _payloadManager.CapturePayload(payload, _capturePivot);
                payload.IsManualControl = true;
                PlaySequence(payload)
                    .OnComplete(() => _payloadManager.DestroyPayload(payload));
            }
        }

        private Tween PlaySequence(Payload payload)
        {
            return DOTween.Sequence()
                .Append(
                    payload.transform.DOMoveY(payload.transform.position.y + 0.3f, 1f)
                        .SetEase(Ease.InOutBack)
                )
                .Append(payload.transform.DOShakePosition(2f, 0.01f, 100, 90))
                .Join(payload.transform.DOShakeRotation(2f, 3f, 100, 90))
                .Append(
                    DOTween.To(
                            () => payload.transform.rotation.eulerAngles,
                            value => payload.transform.rotation = Quaternion.Euler(value),
                            payload.transform.rotation.eulerAngles + Vector3.one * (360 * 2f),
                            5f
                        )
                )
                .Join(
                    payload.transform.DOMove(_targetOut.position, 5f)
                );
        }
    }
}