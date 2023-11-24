using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class DUX : MonoBehaviour, ISaveLoadObject
{
    [Inject] private SaveLoadServise _saveLoadServise;
    [Inject] private DUXWindow _duxWindow;

    private Button _openDUXButton;

    private Sequence _sequence;

    private const string _saveKey = "DUXSave";

    private void Awake()
    {
        Add();
        
        _openDUXButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _openDUXButton.onClick.AddListener(() =>
        {
            _duxWindow.Open();

            if(_sequence != null)
                _sequence.Pause();
        });

        if (_saveLoadServise.HasSave(_saveKey))
            Load();
    }

    private void OnDisable()
    {
        _openDUXButton.onClick.RemoveAllListeners();

        Save();
    }

    public void OpenAccesToDUX()
    {
        _openDUXButton.enabled = true;

        _sequence = DOTween.Sequence();

        _sequence
            .Append(transform.DOScale(1.2f, 1.5f))
            .Append(transform.DOScale(1, 1.5f))
            .SetLoops(-1)
            .Play();
    }

    public void SetEnabled(bool enabled)
    {
        _openDUXButton.enabled = enabled;
    }

    public void Add()
    {
        _saveLoadServise.Add(this);
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.BoolData() { Bool = _openDUXButton.enabled });
    }

    public void Load()
    {
        _openDUXButton.enabled = _saveLoadServise.Load<SaveData.BoolData>(_saveKey).Bool;
    }
}
