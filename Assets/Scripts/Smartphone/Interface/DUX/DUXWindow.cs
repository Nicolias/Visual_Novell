using System;
using UnityEngine;
using UnityEngine.UI;

public class DUXWindow : MonoBehaviour
{
    public event Action OnClosed;

    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
        OnClosed?.Invoke();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
}
