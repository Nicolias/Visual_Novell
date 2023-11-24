using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MessengeIndicator : MonoBehaviour, ISaveLoadObject
{
    [Inject] private SaveLoadServise _saveLoadServise;

    [SerializeField] private Messenger _smartphone;
    [SerializeField] private Image _indicatorImage;
    [SerializeField] private Canvas _selfCanvas;

    private Sequence _indicationSequence;

    private const string _saveKey = "MessengerIndicator";

    private void Awake()
    {
        _indicationSequence = DOTween.Sequence();
        Add();
    }

    private void OnEnable()
    {
        _smartphone.OnNewMessegeRecived += PlayNewMessegeIndicator;
        _smartphone.OnAllMessegeRed += StopPlayIndicator;

        if (_saveLoadServise.HasSave(_saveKey))
            Load();
    }

    private void OnDisable()
    {
        _smartphone.OnNewMessegeRecived -= PlayNewMessegeIndicator;
        _smartphone.OnAllMessegeRed -= StopPlayIndicator;

        Save();
    }

    public void Show()
    {
        _selfCanvas.enabled = true;
    }

    public void Hide()
    {
        _selfCanvas.enabled = false;
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

    public void Add()
    {
        _saveLoadServise.Add(this);
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.BoolData() { Bool = _selfCanvas.enabled });
    }

    public void Load()
    {
        var data = _saveLoadServise.Load<SaveData.BoolData>(_saveKey);
        _selfCanvas.enabled = data.Bool;
    }
}