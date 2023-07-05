using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TellegrammLink : MonoBehaviour
{
    [SerializeField] private string _url;

    private Button _selfButton;

    private void Awake()
    {
        _selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(() => Application.OpenURL(_url));
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
    }
}
