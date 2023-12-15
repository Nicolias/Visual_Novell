using UnityEngine;
using Zenject;

public class Regestration : MonoBehaviour
{
    [SerializeField] private GameObject _menuCanvas;

    private AuntificationServise _auntificationServise;

    [Inject]
    public void Construct(AuntificationServise auntificationServise)
    {
        _auntificationServise = auntificationServise;
    }

    private void OnEnable()
    {
        _auntificationServise.Authorized += OnAuthorized;
    }

    private void OnDisable()
    {
        _auntificationServise.Authorized -= OnAuthorized;
    }

    private void OnAuthorized()
    {
        gameObject.SetActive(false);
        _menuCanvas.SetActive(true);
    }
}