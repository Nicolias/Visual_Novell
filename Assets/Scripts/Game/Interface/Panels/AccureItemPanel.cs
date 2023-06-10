using System;
using UnityEngine;
using UnityEngine.UI;

public class AccureItemPanel : MonoBehaviour
{
    public event Action OnClosed;

    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(() =>
        {
            OnClosed?.Invoke();
            gameObject.SetActive(false);
        });
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
    }
}