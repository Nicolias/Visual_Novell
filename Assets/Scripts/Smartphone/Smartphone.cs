using System;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class Smartphone : MonoBehaviour
{
    public event Action OnNewMessegeReceived;

    [SerializeField] private Button _openButton, _closeButton;
    [SerializeField] private Canvas _selfCanvas;

    [SerializeField] private Messenger _messenger;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener( () =>
        {
            _selfCanvas.enabled = false;
            _openButton.gameObject.SetActive(true);
        });
        _openButton.onClick.AddListener(() =>
        {
            _selfCanvas.enabled = true;
            _openButton.gameObject.SetActive(false);
        });
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
    }

    public void AddNewMessege(MessegeData newMessege)
    {
        OnNewMessegeReceived?.Invoke();

        _messenger.AddNewMessege(newMessege);
    }
}
