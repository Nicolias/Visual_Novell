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

    private const string _saveKey = "BackgroundSave";

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
                .Append(_image.DOColor(new(1, 1, 1, 0), 0.5f))
                .AppendCallback(() => OnPicturChanged?.Invoke())
                .Play();
        }
        else
        {
            sequnce
                .Append(_image.DOColor(new(1, 1, 1, 0), 0.5f))
                .AppendCallback(() => _image.sprite = sprite)
                .Append(_image.DOColor(new(1, 1, 1, 1), 0.5f))
                .AppendCallback(() => OnPicturChanged?.Invoke())
                .Play();
        }
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
