using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class WindowInSmartphone : MonoBehaviour
{
    [SerializeField] private SmartphoneWindows _smartphoneWindowType;

    [field: SerializeField] protected Button OpenButton { get; private set; }

    public SmartphoneWindows Type => _smartphoneWindowType;
    protected SaveLoadServise SaveLoadServise { get; private set; }

    protected Transform SelfTransform { get; private set; }

    [Inject]
    public void Construct(SaveLoadServise saveLoadServise)
    {
        SaveLoadServise = saveLoadServise;
        SelfTransform = transform;
    }

    private void OnEnable()
    {
        OpenButton.onClick.AddListener(OnOpenButtonClicked);
        OnEnabled();        
    }

    private void OnDisable()
    {
        OpenButton.onClick.RemoveListener(OnOpenButtonClicked);
        OnDisabled();
    }

    public virtual void SetEnabled(bool enabled)
    {
        OpenButton.enabled = enabled;
    }

    protected virtual void OnEnabled(){ }

    protected virtual void OnDisabled() { }

    protected abstract void OnOpenButtonClicked();
}