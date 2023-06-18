using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class ChapterCaption : MonoBehaviour
{
    public event Action OnCaptionShowed;

    [SerializeField] private float _duration;
    [SerializeField] private float _showDuration;
    [SerializeField] private TMP_Text _captionText;

    public void ShowText(string text)
    {
        _captionText.text = text;

        DOTween.Sequence()
            .Append(_captionText.DOColor(new(1, 1, 1, 1), _showDuration))
            .AppendInterval(_duration)
            .Append(_captionText.DOColor(new(1, 1, 1, 0), _showDuration))
            .AppendCallback(() => OnCaptionShowed?.Invoke())
            .Play();
    }
}