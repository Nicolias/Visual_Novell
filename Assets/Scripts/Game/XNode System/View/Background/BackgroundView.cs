using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BackgroundView : MonoBehaviour, ISaveLoadObject
{
    public event Action OnPicturChangeStarted;
    public event Action OnPicturChanged;

    [Inject] private SaveLoadServise _saveLoadServise;

    [SerializeField] private Image _image;

    private float _closeDuration = 0.5f;
    private float _showDuration = 0.5f;

    private const string _saveKey = "BackgroundSave";

    public float TurnOffDuratoin => _closeDuration;
    public float ShowDuration => _showDuration;

    private void OnEnable()
    {
        Add();
        
        if(_saveLoadServise.HasSave(_saveKey))
            Load();
    }

    private void OnDisable()
    {
        Save(); 
    }

    public void Replace(Sprite sprite)
    {
        OnPicturChangeStarted?.Invoke();

        var sequnce = DOTween.Sequence();

        if (sprite == null)
        {
            sequnce
                .Append(_image.DOColor(new(1, 1, 1, 0), _closeDuration))
                .AppendCallback(() => _image.sprite = sprite)
                .AppendCallback(() => OnPicturChanged?.Invoke())
                .Play();
        }
        else
        {
            sequnce
                .Append(_image.DOColor(new(1, 1, 1, 0), _closeDuration))
                .AppendCallback(() => _image.sprite = sprite)
                .Append(_image.DOColor(new(1, 1, 1, 1), _showDuration))
                .AppendCallback(() => OnPicturChanged?.Invoke())
                .Play();
        }
    }

    public void Add()
    {
        _saveLoadServise.Add(this);
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
