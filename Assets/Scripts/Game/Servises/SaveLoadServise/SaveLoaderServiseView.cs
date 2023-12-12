using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SaveLoaderServiseView : MonoBehaviour
{
    [Inject] private SaveLoadServise _saveLoadServise;

    [SerializeField] private Button _saveButton;

    private void OnEnable()
    {
        _saveButton.onClick.AddListener(Save);
    }

    private void OnDisable()
    {
        _saveButton.onClick.RemoveListener(Save);
    }

    private void Save()
    {
        _saveLoadServise.SaveAll();
    }
}