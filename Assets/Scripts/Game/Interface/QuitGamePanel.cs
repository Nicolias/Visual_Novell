using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitGamePanel : MonoBehaviour
{
    [SerializeField] private Button _exitButton;

    [SerializeField] private Button _completeExiteButton;
    [SerializeField] private Button _exitPanelCloseButton;

    [SerializeField] private Canvas _selfCanvas;

    private Action _quitAction;

    private void OnEnable()
    {
        if (_exitButton != null)
        {
            _exitButton.onClick.AddListener(Show);
            _completeExiteButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        }
        else
        {
            _completeExiteButton.onClick.AddListener(Application.Quit);
        }

        _exitPanelCloseButton.onClick.AddListener(() => _selfCanvas.enabled = false);
    }

    private void OnDisable()
    {
        if (_exitButton != null)
            _exitButton.onClick.RemoveAllListeners();
        _exitPanelCloseButton.onClick.RemoveAllListeners();
        _completeExiteButton.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        _selfCanvas.enabled = true;
    }
}