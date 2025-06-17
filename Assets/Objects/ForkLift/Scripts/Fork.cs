using System;
using UnityEngine;

namespace Forklift
{
    public class Fork : MonoBehaviour
    {
        [SerializeField] private Transform _forkTransform;
        [SerializeField] private Transform _minPositionTransform;
        [SerializeField] private Transform _maxPositionTransform;

        public float Speed { get; set; }
        // public float Torque { get; private set; }

        private float _minPosition;
        private float _maxPosition;

        private void Awake()
        {
            _minPosition = _minPositionTransform.localPosition.y;
            _maxPosition = _maxPositionTransform.localPosition.y;
        }

        private void FixedUpdate()
        {
            var currentPosition = _forkTransform.localPosition.y + Speed * Time.fixedDeltaTime;
            var positionFactor = Mathf.InverseLerp(_minPosition, _maxPosition, currentPosition);
            var position = Mathf.Lerp(_minPosition, _maxPosition, Mathf.Clamp01(positionFactor));

            _forkTransform.localPosition = new Vector3(
                _forkTransform.localPosition.x,
                position,
                _forkTransform.localPosition.z
            );
        }
    }
}