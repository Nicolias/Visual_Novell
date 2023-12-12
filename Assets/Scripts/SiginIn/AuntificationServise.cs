using Unity.Services.Core;
using UnityEngine;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using TMPro;
using System.Linq;
using Zenject;
using UnityEngine.Events;

public class AuntificationServise : MonoBehaviour
{
    private TMP_InputField _userName;
    private TMP_InputField _password;

    private SaveLoadServise _saveLoadServise;
    private TimesOfDayServise _timesOfDayServise;
    private StaticData _staticData;

    public event UnityAction Authorized;

    [Inject]
    public void Construct(SaveLoadServise saveLoadServise, TimesOfDayServise timesOfDayServise, StaticData staticData)
    {
        _saveLoadServise = saveLoadServise;
        _timesOfDayServise = timesOfDayServise;
        _staticData = staticData;
    }

    public async void Initialize(TMP_InputField userName, TMP_InputField password)
    {
        _userName = userName;
        _password = password;

        await UnityServices.InitializeAsync();

        SetupEvents();

        if (AuthenticationService.Instance.PlayerId != "")
            SignOut();

        AuthenticationService.Instance.SignedOut += Instance_SignedOut;
    }

    private void OnDestroy()
    {
        AuthenticationService.Instance.SignedOut -= Instance_SignedOut;
    }

    private void Instance_SignedOut()
    {
        Debug.Log("Выход");
    }

    public void SignIn()
    {
        if (IsNicknameValid() == false || IsPasswordValid() == false)
        {
            Debug.Log("Некоректный логин или пароль");
            return;
        }

        SignIn(_userName.text, _password.text);
    }

    public void SignUp()
    {
        if (IsNicknameValid() == false || IsPasswordValid() == false)
        {
            Debug.Log("Некоректный логин или пароль");
            return;
        }

        SignUp(_userName.text, _password.text);
    }

    public void SignOut()
    {
        AuthenticationService.Instance.SignOut();
    }

    private async Task SignUp(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log("Регистрация is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }

        await InitializeAndLoadScene();
    }

    private async Task SignIn(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            Debug.Log("Логин is successful.");
            await InitializeAndLoadScene();
        }
        catch
        {
            Debug.Log("Неправильный логин или пароль");
        }
    }

    private async Task InitializeAndLoadScene()
    {
        Debug.Log(AuthenticationService.Instance.PlayerId);

        await _saveLoadServise.Initialize();
        await _timesOfDayServise.Initialize();
        _staticData.Initialize();

        Authorized?.Invoke();
    }

    private void SetupEvents()
    {
        AuthenticationService.Instance.SignedIn += () => {
            // Shows how to get a playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

            // Shows how to get an access token
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");
        };

        AuthenticationService.Instance.SignedOut += () => {
            Debug.Log("Player signed out.");
        };

        AuthenticationService.Instance.Expired += () =>
        {
            Debug.Log("Player session could not be refreshed and expired.");
        };
    }

    private bool IsNicknameValid()
    {
        string nickname = _userName.text;

        if (nickname.Any(char.IsLower) == false || nickname.Any(char.IsUpper) == false)
            return false;

        if (!nickname.Any(char.IsUpper))
            return false;

        if (!nickname.Any(char.IsDigit))
            return false;

        char[] allowedSymbols = { '-', '_', '.', '@' };
        if (!nickname.Any(c => allowedSymbols.Contains(c)))
            return false;

        return true;
    }

    private bool IsPasswordValid()
    {
        string password = _password.text;

        if (!password.Any(char.IsLower))
            return false;

        if (!password.Any(char.IsUpper))
            return false;

        if (!password.Any(char.IsDigit))
            return false;

        char[] allowedSymbols = { '-', '_', '!' };
        if (!password.Any(c => allowedSymbols.Contains(c)))
            return false;

        return true;
    }
}