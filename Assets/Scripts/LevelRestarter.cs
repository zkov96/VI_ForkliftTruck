using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class LevelRestarter : MonoBehaviour
{
    [Inject] private ForkLift.ForkLift _forklift;

    private CanvasGroup _canvasGroup;
    private float _startRestartTime = -1;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }

    private void Update()
    {
        if (_forklift.Fuel <= 0 && _startRestartTime <= 0)
        {
            _startRestartTime = Time.time;
            DOTween.To(() => _canvasGroup.alpha, value => _canvasGroup.alpha = value, 1, 1)
                .SetDelay(3);
        }

        if (_startRestartTime >= 0 && Time.time - _startRestartTime > 10)
        {
            SceneManager.LoadScene(0);
        }
    }
}