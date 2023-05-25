using UnityEngine;
using UnityEngine.UI;

public class MapWindow : MonoBehaviour
{
    [SerializeField] private Canvas _selfCanvas;
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(() => _selfCanvas.enabled = false);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
    }
}
