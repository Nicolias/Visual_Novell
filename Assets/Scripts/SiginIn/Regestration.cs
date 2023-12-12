using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class Regestration : MonoBehaviour
{
    [SerializeField] private TMP_InputField _userName;
    [SerializeField] private TMP_InputField _password;

    [SerializeField] private Button _signInButton;
    [SerializeField] private Button _signUpButton;
    [SerializeField] private Button _signOutButton;

    [SerializeField] private GameObject _menuCanvas;

    private AuntificationServise _auntificationServise;

    [Inject]
    public void Construct(AuntificationServise auntificationServise)
    {
        _auntificationServise = auntificationServise;

        auntificationServise.Initialize(_userName, _password);
    }

    private void OnEnable()
    {
        _signInButton.onClick.AddListener(SignIn);
        _signUpButton.onClick.AddListener(SignUp);
        _signOutButton.onClick.AddListener(SignOut);

        _auntificationServise.Authorized += OnAuthorized;
    }

    private void OnDisable()
    {
        _signInButton.onClick.RemoveListener(SignIn);
        _signUpButton.onClick.RemoveListener(SignUp);
        _signOutButton.onClick.RemoveListener(SignOut);

        _auntificationServise.Authorized -= OnAuthorized;
    }

    private void OnAuthorized()
    {
        gameObject.SetActive(false);
        _menuCanvas.SetActive(true);
    }

    private void SignOut()
    {
        _auntificationServise.SignOut();
    }

    private void SignUp()
    {
        _auntificationServise.SignUp();
    }

    private void SignIn()
    {
        _auntificationServise.SignIn();
    }

}
