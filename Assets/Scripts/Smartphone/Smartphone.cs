using System;
using UnityEngine;
using UnityEngine.UI;

public class Smartphone : MonoBehaviour
{
    public event Action OnNewMessegeReceived;    

    [SerializeField] private Button _openButton, _closeButton;
    [SerializeField] private Canvas _selfCanvas;

    [SerializeField] private DialogSpeechView _dialogSpeechView;

    [SerializeField] private Messenger _messenger;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener( () =>
        {
            _selfCanvas.enabled = false;
            _openButton.image.color = new(1, 1, 1, 1);
            _dialogSpeechView.gameObject.SetActive(true);
        });
        _openButton.onClick.AddListener(() =>
        {
            _selfCanvas.enabled = true;
            _openButton.image.color = new(1, 1, 1, 0);
            _dialogSpeechView.gameObject.SetActive(false);
        });
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
    }

    public void AddNewMessege(MessegeData newMessege, Action playActionAfterMessegeRed)
    {
        OnNewMessegeReceived?.Invoke();

        _messenger.AddNewMessege(newMessege, playActionAfterMessegeRed);
    }
}
