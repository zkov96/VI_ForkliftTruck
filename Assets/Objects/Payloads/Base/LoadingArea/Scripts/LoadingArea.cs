using UnityEngine;
using Zenject;

namespace Payloads.Base.LoadingArea
{
    [RequireComponent(typeof(BoxCollider))]
    public class LoadingArea : MonoBehaviour
    {
        private static readonly int Play_AK = Animator.StringToHash("Play");

        [Inject] private PayloadManager _payloadManager { get; set; }


        [SerializeField] private Payload _payloadPrefab;
        [SerializeField] private float _respawnTime;
        [SerializeField] private Transform _spawnPivot;

        [SerializeField] private Animator _animator;

        private float _lastCollisionTime;
        protected Payload _capturedPayload;


        private void Start()
        {
            _lastCollisionTime = Time.time;
        }

        private void OnTriggerStay(Collider other)
        {
            _lastCollisionTime = Time.time;
        }

        private void Update()
        {
            if (_capturedPayload == null && Time.time - _lastCollisionTime > _respawnTime)
            {
                _lastCollisionTime = Time.time;
                Respawn();
            }
        }

        private void Respawn()
        {
            _animator.SetTrigger(Play_AK);
        }

        private void OnSpawnAnimationStart()
        {
            _capturedPayload = _payloadManager.AllocatePayload(_payloadPrefab);
            _payloadManager.CapturePayload(_capturedPayload, _spawnPivot, true);
            
            _capturedPayload.transform.localPosition = Vector3.zero;
            _capturedPayload.transform.localRotation = Quaternion.Euler(Vector3.zero);

            _capturedPayload.IsManualControl = true;
        }

        private void OnSpawnAnimationComplete()
        {
            if (_capturedPayload != null)
            {
                _capturedPayload.IsManualControl = false;
                if (_capturedPayload is PayloadContainer payloadContainer)
                {
                    payloadContainer.FreeInnerPayload();
                }

                _payloadManager.FreePayload(_capturedPayload);
                _capturedPayload = null;
            }
        }
    }
}