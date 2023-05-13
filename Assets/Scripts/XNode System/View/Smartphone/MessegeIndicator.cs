using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MessegeIndicator : MonoBehaviour
{
    [SerializeField] private Smartphone _smartphone;
    [SerializeField] public Image _indicatorImage;

    private Sequence _indicationSequence;

    private void Awake()
    {
        _indicationSequence = DOTween.Sequence();
    }

    private void OnEnable()
    {
        _smartphone.OnNewMessegeReceived += PlayNewMessegeIndicator;
    }

    private void OnDisable()
    {
        _smartphone.OnNewMessegeReceived -= PlayNewMessegeIndicator;
    }

    private void PlayNewMessegeIndicator()
    {
        _indicationSequence
            .Append(_indicatorImage.DOColor(new(1, 1, 1, 1), 1))
            .Append(_indicatorImage.DOColor(new(1, 1, 1, 0), 1))
            .SetLoops(-1)
            .Play();
    }
}