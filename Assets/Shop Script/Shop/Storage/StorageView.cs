using TMPro;
using UnityEngine;

public abstract class StorageView : MonoBehaviour
{
    [SerializeField] private TMP_Text _storageValueText;

    private IStorageView _storage;

    private void Awake()
    {
        _storage = GetStorage();
    }

    private void OnEnable()
    {
        _storage.ValueChanged += UpdateView;
        UpdateView(_storage.CurrentValue);
    }

    private void OnDisable()
    {
        _storage.ValueChanged -= UpdateView;
    }

    private void UpdateView(int value)
    {
        _storageValueText.text = value.ToString();
    }

    protected abstract IStorageView GetStorage();
}
