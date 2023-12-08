using Unity.Services.Core;
using UnityEngine;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class Regestration : MonoBehaviour
{
    [SerializeField] private TMP_InputField _userName;
    [SerializeField] private TMP_InputField _password;

    [SerializeField] private Button _siginInButton;

    private string _saveKey = "Registration";

    private async void Awake()
    { 
        await UnityServices.InitializeAsync();

        SetupEvents();

        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync("DmoqUGS-7", "DamoqUGS-07!");
            
            Debug.Log("SignIn is successful.");
        }
        catch (AuthenticationException ex)
        {
            await SignUpWithUsernamePassword("DmoqUGS-7", "DamoqUGS-07!");
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    private void OnEnable()
    {
        _siginInButton.onClick.AddListener(SignInWithUsernamePassword);
    }

    private void OnDisable()
    {
        _siginInButton.onClick.RemoveListener(SignInWithUsernamePassword);
        AuthenticationService.Instance.SignOut();
    }

    //private void Update()
    //{
    //    if (Input.GetKey(KeyCode.UpArrow))
    //        _saveLoadServise.Save(_saveKey, "Marat");
    //}

    async Task SignUpWithUsernamePassword(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log("SignUp is successful.");
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
    }

    private void SignInWithUsernamePassword()
    {
        SiginIn();

        async Task SiginIn()
        {
            try
            {
                await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(_userName.text, _password.text);
                Debug.Log("SignIn is successful.");
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
        }
    }

    async Task SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in anonymously succeeded!");

            // Shows how to get the playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

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
    }

    void SetupEvents()
    {
        AuthenticationService.Instance.SignedIn += () => {
            // Shows how to get a playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

            // Shows how to get an access token
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");

        };

        AuthenticationService.Instance.SignInFailed += (err) => {
            Debug.LogError(err);
        };

        AuthenticationService.Instance.SignedOut += () => {
            Debug.Log("Player signed out.");
        };

        AuthenticationService.Instance.Expired += () =>
        {
            Debug.Log("Player session could not be refreshed and expired.");
        };
    }
}
