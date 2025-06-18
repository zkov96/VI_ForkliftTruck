using UnityEngine;
using UnityEngine.UI;

namespace ForkLift.Panels
{
    public class EngineStatus : MonoBehaviour
    {
        [SerializeField] private Image _engineStatusImage;

        [SerializeField] private Color _engineStatusOffColor;
        [SerializeField] private Color _engineStatusOnColor;

        private bool _isOn = false;

        public bool IsOn
        {
            get => _isOn;
            set
            {
                _isOn = value;
                _engineStatusImage.color = value ? _engineStatusOnColor : _engineStatusOffColor;
            }
        }

        private void Start()
        {
            IsOn = false;
        }
    }
}