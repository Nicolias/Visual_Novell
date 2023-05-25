using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundView : MonoBehaviour
{
    public event Action OnPicturChange;

    [SerializeField] private Image _image;

    public void Replace(Sprite sprite)
    {
        var sequnce = DOTween.Sequence();

        if (sprite == null)
        {
            sequnce
                .Append(_image.DOColor(new(1, 1, 1, 0), 0.5f))
                .AppendCallback(() => OnPicturChange?.Invoke())
                .Play();
        }
        else
        {
            sequnce
                .Append(_image.DOColor(new(1, 1, 1, 0), 0.5f))
                .AppendCallback(() => _image.sprite = sprite)
                .Append(_image.DOColor(new(1, 1, 1, 1), 0.5f))
                .AppendCallback(() => OnPicturChange?.Invoke())
                .Play();
        }
    }
}
