using UnityEngine;
using Zenject;

public class Regestration : MonoBehaviour
{
    [SerializeField] private LoadingWindow _loadingWindow;
    [SerializeField] private GameObject _menuCanvas;

    private AuntificationServise _auntificationServise;

    [Inject]
    public void Construct(AuntificationServise auntificationServise)
    {
        _auntificationServise = auntificationServise;
    }

    private void OnEnable()
    {
        _auntificationServise.Signing += OnSigning;
    }

    private void OnDisable()
    {
        _auntificationServise.Signing -= OnSigning;
    }

    private void OnSigning()
    {
        _loadingWindow.Show();
        _loadingWindow.Authorized += OnAuthorized;
    }

    private void OnAuthorized()
    {
        _loadingWindow.Authorized -= OnAuthorized;

        if (_auntificationServise.IsAuthorized)
        {
            _menuCanvas.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
