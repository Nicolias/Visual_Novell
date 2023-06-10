using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Smartphone : MonoBehaviour
{
    public event Action OnClosed;

    [SerializeField] private Button _openButton, _closeButton;
    [SerializeField] private Canvas _selfCanvas;

    [SerializeField] private DialogSpeechView _dialogSpeechView;

    [SerializeField] private Messenger _messenger;
    [SerializeField] private DUX _dux;

    [SerializeField] private TMP_Text _clockText;

    private bool _isDUXTutorialShow = false;

    public Messenger Messenger => _messenger;
    public Canvas SelfCanvas => _selfCanvas;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(Hide);
        _openButton.onClick.AddListener(Show);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();        
    }

    public void SetTime(string hour, string minute)
    {
        _clockText.text = $"{hour}:{minute}";
    }

    public void OpenAccesToDUX()
    {
        if (_isDUXTutorialShow)
            return;

        _dux.OpenAccesToDUX();
        _isDUXTutorialShow = true;
    }

    private void Show()
    {
        _selfCanvas.enabled = true;
        _openButton.image.color = new(1, 1, 1, 0);
        _dialogSpeechView.gameObject.SetActive(false);
    }

    public void Hide()
    {
        _selfCanvas.enabled = false;
        _openButton.image.color = new(1, 1, 1, 1);
        _dialogSpeechView.gameObject.SetActive(true);
        OnClosed?.Invoke();
    }
}
