using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Agava.YandexGames;
using System;

public class AdsServise : MonoBehaviour
{
    [SerializeField] private Button _showAdsButton;
    [SerializeField] private int _prizeForWatchAds;

    private AudioServise _audioServise;
    private StaticData _staticData;

    public event Action AdsShowed;

    [Inject]
    public void Construct(AudioServise audioServise, StaticData staticData)
    {
        _audioServise = audioServise;
        _staticData = staticData;
    }

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
        PlayerAccount.AuthorizedInBackground += OnAuthorizedInBackground;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();

        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.StartAuthorizationPolling(1500);

        OnShowStickyAdButtonClick();
    }

    private void OnEnable()
    {
        _showAdsButton.onClick.AddListener(() => ShowAdAndAccrue(100));
    }

    private void OnDisable()
    {
        _showAdsButton.onClick.RemoveAllListeners();

        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        PlayerAccount.AuthorizedInBackground -= OnAuthorizedInBackground;
    }

    private void ShowAdAndAccrue(int prize)
    {
        _audioServise.Silence(true);

        VideoAd.Show(onRewardedCallback: () =>
        {
            prize = 0;
            _audioServise.Silence(false);
        });
    }

    public void OnShowInterstitialButtonClick()
    { 
        InterstitialAd.Show(onCloseCallback: (boo) =>
        {
            _staticData.OnAdsShowed();
            AdsShowed?.Invoke();
        });
    }

    public void OnShowStickyAdButtonClick()
    {
        StickyAd.Show();
    }

    public void OnHideStickyAdButtonClick()
    {
        StickyAd.Hide();
    }

    public void OnAuthorizeButtonClick()
    {
        PlayerAccount.Authorize();
    }

    public void OnRequestPersonalProfileDataPermissionButtonClick()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();
    }

    public void OnGetProfileDataButtonClick()
    {
        PlayerAccount.GetProfileData((result) =>
        {
            string name = result.publicName;
            if (string.IsNullOrEmpty(name))
                name = "Anonymous";
            Debug.Log($"My id = {result.uniqueID}, name = {name}");
        });
    }

    public void OnSetLeaderboardScoreButtonClick()
    {
        Leaderboard.SetScore("PlaytestBoard", UnityEngine.Random.Range(1, 100));
    }

    public void OnGetLeaderboardEntriesButtonClick()
    {
        Leaderboard.GetEntries("PlaytestBoard", (result) =>
        {
            Debug.Log($"My rank = {result.userRank}");
            foreach (var entry in result.entries)
            {
                string name = entry.player.publicName;
                if (string.IsNullOrEmpty(name))
                    name = "Anonymous";
                Debug.Log(name + " " + entry.score);
            }
        });
    }

    public void OnGetLeaderboardPlayerEntryButtonClick()
    {
        Leaderboard.GetPlayerEntry("PlaytestBoard", (result) =>
        {
            if (result == null)
                Debug.Log("Player is not present in the leaderboard.");
            else
                Debug.Log($"My rank = {result.rank}, score = {result.score}");
        });
    }

    public void OnSetCloudSaveDataButtonClick(string text)
    {
        PlayerAccount.SetCloudSaveData(text);
    }

    public void OnGetCloudSaveDataButtonClick()
    {
        string text;

        PlayerAccount.GetCloudSaveData((data) => text = data);
    }

    public void OnGetEnvironmentButtonClick()
    {
        Debug.Log($"Environment = {JsonUtility.ToJson(YandexGamesSdk.Environment)}");
    }

    private void OnAuthorizedInBackground()
    {
        Debug.Log($"{nameof(OnAuthorizedInBackground)} {PlayerAccount.IsAuthorized}");
    }
}