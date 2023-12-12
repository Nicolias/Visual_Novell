using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Smartphone : MonoBehaviour, ISaveLoadObject
{
    [SerializeField] private Button _openButton, _closeButton;
    [SerializeField] private Canvas _selfCanvas;

    [SerializeField] private DialogSpeechView _dialogSpeechView;

    [SerializeField] public List<WindowInSmartphone> _apps;

    [SerializeField] private Messenger _messenger;
    [SerializeField] private DUX _dux;

    [SerializeField] private Clock _clock;

    private SaveLoadServise _saveLoadServise;

    private bool _isDUXTutorialShow = false;
    private const string _saveKey = "SmartphoneSave";

    public Messenger Messenger => _messenger;
    public Canvas SelfCanvas => _selfCanvas;

    public event Action Closed;

    [Inject]
    public void Construct(SaveLoadServise saveLoadServise)
    {
        _saveLoadServise = saveLoadServise;
    }

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(Hide);
        _openButton.onClick.AddListener(Show);

        Add();
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
        _openButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        if (_saveLoadServise.HasSave(_saveKey))
            Load();
    }

    public void SetTime(int hour, int minute)
    {
        _clock.SetTime(hour, minute);
    }

    public void OpenAccesToDUX()
    {
        if (_isDUXTutorialShow)
            return;

        _dux.OpenAccesToDUX();
        _isDUXTutorialShow = true;
    }

    public void ChangeEnabled(Dictionary.Dictionary<SmartphoneWindows, bool> windowsEnabled)
    {
        for (int i = 0; i < windowsEnabled.Count; i++)
        {
            SmartphoneWindows appType = windowsEnabled.GetKey(i);

            GetWindow(appType).SetOpenButtonEnabled(windowsEnabled.GetValue(i));
        }
    }

    public void ShowGuid(SmartphoneWindows window)
    {
        GetWindow(window).ShowGuid();
    }

    private WindowInSmartphone GetWindow(SmartphoneWindows type)
    {
        if (_apps.Exists(app => app.Type == type))
            return _apps.Find(app => app.Type == type);

        return null;
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
        Closed?.Invoke();
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.SmartphoneData()
        {
            IsDuxTutorialComplete = _isDUXTutorialShow
        });

        for (int i = 0; i < _apps.Count; i++)
            _saveLoadServise.Save(_saveKey + i, new SaveData.BoolData() { Bool = _apps[i].OpenButtonEnable });
    }

    public void Load()
    {
        var data = _saveLoadServise.Load<SaveData.SmartphoneData>(_saveKey);

        _isDUXTutorialShow = data.IsDuxTutorialComplete;

        for (int i = 0; i < _apps.Count; i++)
            _apps[i].SetOpenButtonEnabled(_saveLoadServise.Load<SaveData.BoolData>(_saveKey + i).Bool);
    }

    public void Add()
    {
        _saveLoadServise.Add(this);
    }
}
