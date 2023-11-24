using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class WindowInSmartphone : MonoBehaviour
{
    [SerializeField] private SmartphoneWindows _smartphoneWindowType;

    [SerializeField] private Button _openButton;
    [SerializeField] private GameObject _guidPanel;

    protected SaveLoadServise SaveLoadServise { get; private set; }

    protected Transform SelfTransform { get; private set; }

    public SmartphoneWindows Type => _smartphoneWindowType;
    public bool OpenButtonEnable => _openButton.enabled;

    [Inject]
    public void Construct(SaveLoadServise saveLoadServise)
    {
        SaveLoadServise = saveLoadServise;
        SelfTransform = transform;
    }

    private void OnEnable()
    {
        _openButton.onClick.AddListener(OnOpenButtonClicked);
        OnEnabled();        
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(OnOpenButtonClicked);
        OnDisabled();
    }

    public virtual void SetOpenButtonEnabled(bool enabled)
    {
        _openButton.enabled = enabled;
    }

    public void ShowGuid()
    {
        if(_guidPanel != null)
            _guidPanel.SetActive(true);
    }

    protected virtual void OnEnabled(){ }

    protected virtual void OnDisabled() { }

    protected abstract void OnOpenButtonClicked();
}