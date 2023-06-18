using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitGamePanel : MonoBehaviour
{
    [SerializeField] private Button _exitButton;

    [SerializeField] private Button _completeExiteButton;
    [SerializeField] private Button _exitPanelCloseButton;

    [SerializeField] private Canvas _selfCanvas;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(() => _selfCanvas.enabled = true);
        _exitPanelCloseButton.onClick.AddListener(() => _selfCanvas.enabled = false);
        _completeExiteButton.onClick.AddListener(() => SceneManager.LoadScene(0));
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveAllListeners();
        _exitPanelCloseButton.onClick.RemoveAllListeners();
        _completeExiteButton.onClick.RemoveAllListeners();
    }
}