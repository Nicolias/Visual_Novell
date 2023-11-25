using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class InterstionPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Smartphone _smartphone;

    [SerializeField] private bool _isInUnityEditor;

    private AdsServise _adsServise;
    private WaitForSeconds _waitOneSeconde;

    public event UnityAction AdsShowed;

    [Inject]
    public void Construct(AdsServise adsServise)
    {
        _adsServise = adsServise;
        _waitOneSeconde = new WaitForSeconds(1f);
    }

    public void ShowAds()
    {
        gameObject.SetActive(true);
        StartCoroutine(StartCountDown());
    }

    private IEnumerator StartCountDown()
    {
        for (int i = 3; i >= 0; i--)
        {
            _scoreText.text = i.ToString();
            yield return _waitOneSeconde;
        }

        if (_isInUnityEditor)
        {
            OnAdsShowed();
        }
        else
        {
            _adsServise.OnShowInterstitialButtonClick();
            _adsServise.AdsShowed += OnAdsShowed;
        }
    }

    private void OnAdsShowed()
    {
        gameObject.SetActive(false);
        _adsServise.AdsShowed -= OnAdsShowed;

        _smartphone.Show();
        AdsShowed?.Invoke();
    }
}
