using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CellView : MonoBehaviour
{
    [SerializeField] private Transform _subCellsContainer;

    private TMP_Text _textOnCell;
    private Button _selfButton;

    public Transform SubcellsContainer => _subCellsContainer;

    public event UnityAction Clicked;

    private void Awake()
    {
        if (_selfButton == null)
            _selfButton = gameObject.GetComponent<Button>();
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(OnClicked);

        if(_subCellsContainer.gameObject.activeInHierarchy)
            SwitchSubcellsEnable();
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveListener(OnClicked);
    }

    public void Initialize(string textOnCell)
    {
        if(_textOnCell == null)
            _textOnCell = GetComponentInChildren<TMP_Text>();

        if (_selfButton == null)
            _selfButton = gameObject.GetComponent<Button>();

        _textOnCell.text = textOnCell;

        _subCellsContainer.SetParent(transform.parent);
    }

    public void Destory()
    {
        DestroyImmediate(gameObject);
    }

    public void SetInteractable(bool isEnabled)
    {
        _selfButton.interactable = isEnabled;
    }

    public void SwitchSubcellsEnable()
    {
        ChangeSubcellsEnable(!_subCellsContainer.gameObject.activeInHierarchy);
    }

    public void ChangeSubcellsEnable(bool isEnabled)
    {
        _subCellsContainer.gameObject.SetActive(isEnabled);
    }

    private void OnClicked()
    {
        Clicked?.Invoke();
    }
}
