using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class Smartphone : MonoBehaviour, ISaveLoadObject
{
    public event Action OnClosed;

    [Inject] private SaveLoadServise _saveLoadServise;
    [Inject] private Map _map;

    [SerializeField] private Button _openButton, _closeButton;
    [SerializeField] private Canvas _selfCanvas;

    [SerializeField] private DialogSpeechView _dialogSpeechView;

    [SerializeField] private Messenger _messenger;
    [SerializeField] private DUX _dux;

    [SerializeField] private TMP_Text _clockText;

    private bool _isDUXTutorialShow = false;
    private DateTime _currentTime = new(1,1,1,23,34,0);
    private const string _saveKey = "SmartphoneSave";

    public Messenger Messenger => _messenger;
    public Canvas SelfCanvas => _selfCanvas;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(Hide);
        _openButton.onClick.AddListener(Show);

        if (_saveLoadServise.HasSave(_saveKey))
            Load();
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
        _openButton.onClick.RemoveAllListeners();

        Save();
    }

    private void Start()
    {
        if(_saveLoadServise.HasSave(_saveKey) == false)
            SetTime(_currentTime.Hour, _currentTime.Minute);   
    }

    public void SetTime(int hour, int minute)
    {
        _currentTime = DateTime.MinValue;
        _currentTime = _currentTime.AddHours(hour);
        _currentTime = _currentTime.AddMinutes(minute);
        
        _clockText.text = $"{_currentTime:HH}:{_currentTime:mm}";
    }

    public void OpenAccesToDUX()
    {
        if (_isDUXTutorialShow)
            return;

        _dux.OpenAccesToDUX();
        _isDUXTutorialShow = true;
    }

    public void ChangeEnabled(List<(SmartphoneWindows, bool)> windowsEnabled)
    {
        foreach (var windowEnabled in windowsEnabled)
        {
            switch (windowEnabled.Item1)
            {
                case SmartphoneWindows.Map:
                    _map.SetEnabled(windowEnabled.Item2);
                    break;
                case SmartphoneWindows.DUX:
                    _dux.SetEnabled(windowEnabled.Item2);
                    break;
            }
        }
    }

    private void Show()
    {
        _selfCanvas.enabled = true;
        _openButton.image.color = new(1, 1, 1, 0);
        _dialogSpeechView.gameObject.SetActive(false);
    }

    public void Hide()
    {
        _selfCanvas.enabled = false;
        _openButton.image.color = new(1, 1, 1, 1);
        _dialogSpeechView.gameObject.SetActive(true);
        OnClosed?.Invoke();
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.SmartphoneData()
        {
            Hors = _currentTime.Hour,
            Minuts = _currentTime.Minute,
            IsDuxTutorialComplete = _isDUXTutorialShow
        });
    }

    public void Load()
    {
        var data = _saveLoadServise.Load<SaveData.SmartphoneData>(_saveKey);

        SetTime(data.Hors, data.Minuts);
        _isDUXTutorialShow = data.IsDuxTutorialComplete;
    }
}
