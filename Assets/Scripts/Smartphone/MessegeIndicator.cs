using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MessegeIndicator : MonoBehaviour
{
    [SerializeField] private Messenger _smartphone;
    [SerializeField] private Image _indicatorImage;

    private Sequence _indicationSequence;

    private void Awake()
    {
        _indicationSequence = DOTween.Sequence();
    }

    private void OnEnable()
    {
        _smartphone.OnNewMessegeRecived += PlayNewMessegeIndicator;
        _smartphone.OnAllMessegeRed += StopPlayIndicator;
    }

    private void OnDisable()
    {
        _smartphone.OnNewMessegeRecived -= PlayNewMessegeIndicator;
        _smartphone.OnAllMessegeRed -= StopPlayIndicator;
    }

    private void PlayNewMessegeIndicator()
    {
        _indicationSequence
            .Append(_indicatorImage.DOColor(new(1, 1, 1, 1), 1))
            .Append(_indicatorImage.DOColor(new(1, 1, 1, 0), 1))
            .SetLoops(-1)
            .Play();
    }

    private void StopPlayIndicator()
    {
        _indicationSequence.Restart();
        _indicationSequence.Pause();
    }
}