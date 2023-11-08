using DG.Tweening;
using Zenject;

public class DUX : WindowInSmartphone
{
    [Inject] private DUXWindow _duxWindow;

    private string _saveKey = "DUXSave";

    private Sequence _sequence;

    public void OpenAccesToDUX()
    {
        SetEnabled(true);

        _sequence = DOTween.Sequence();

        _sequence
            .Append(transform.DOScale(1.2f, 1.5f))
            .Append(transform.DOScale(1, 1.5f))
            .SetLoops(-1)
            .Play();
    }

    public void Save()
    {
        SaveLoadServise.Save(_saveKey, new SaveData.BoolData() { Bool = OpenButton.enabled });
    }

    public void Load()
    {
        OpenButton.enabled = SaveLoadServise.Load<SaveData.BoolData>(_saveKey).Bool;
    }

    protected override void OnEnabled()
    {
        if (SaveLoadServise.HasSave(_saveKey))
            Load();
    }

    protected override void OnDisabled()
    {
        Save();
    }

    protected override void OnOpenButtonClicked()
    {
        _duxWindow.Open();

        if (_sequence != null)
            _sequence.Pause();
    }
}