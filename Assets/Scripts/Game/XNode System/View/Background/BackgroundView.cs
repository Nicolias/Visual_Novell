using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BackgroundView : MonoBehaviour, ISaveLoadObject
{
    public event Action OnPicturChanged;

    [Inject] private SaveLoadServise _saveLoadServise;

    [SerializeField] private Image _image;
    [SerializeField] private CanvasGroup _canvasGroup;

    private float _closeDuration = 0.5f;
    private float _showDuration = 0.5f;

    private const string _saveKey = "BackgroundSave";

    public float TurnOffDuratoin => _closeDuration;
    public float ShowDuration => _showDuration;

    private void OnEnable()
    {
        if(_saveLoadServise.HasSave(_saveKey))
            Load();
    }

    private void OnDisable()
    {
        Save(); 
    }

    public void Replace(Sprite sprite)
    {
        var sequnce = DOTween.Sequence();

        if (sprite == null)
        {
            sequnce
                .Append(DOVirtual.Float(0, 1, _closeDuration, v => _canvasGroup.alpha = v))
                .AppendCallback(() => OnPicturChanged?.Invoke())
                .Play();
        }
        else
        {
            sequnce
                .Append(DOVirtual.Float(1, 0, _closeDuration, v => _canvasGroup.alpha = v))
                .AppendCallback(() => _image.sprite = sprite)
                .Append(DOVirtual.Float(0, 1, _closeDuration, v => _canvasGroup.alpha = v))
                .AppendCallback(() => OnPicturChanged?.Invoke())
                .Play();
        }
    }

    public void Show(Sprite sprite)
    {
        _canvasGroup.alpha = 0;
        _image.sprite = sprite;

        DOTween.Sequence()
            .Append(DOVirtual.Float(0, 1, _closeDuration, v => _canvasGroup.alpha = v))
            .AppendCallback(() => OnPicturChanged?.Invoke())
            .Play();
    }

    public void Save()
    {
        _saveLoadServise.Save<SaveData.ImageData>(_saveKey, new() { Sprite = _image.sprite });
    }

    public void Load()
    {
        var data = _saveLoadServise.Load<SaveData.ImageData>(_saveKey);
        Replace(data.Sprite);
    }
}
