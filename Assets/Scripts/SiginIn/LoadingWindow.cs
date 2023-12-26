using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadingWindow : MonoBehaviour
{
    [SerializeField] private Slider _loadSlider;

    [SerializeField] private int _loadTime;
    private float _waitSeconds = 0.01f;

    public event UnityAction Authorized;

    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(StartLoad());
    }

    private IEnumerator StartLoad()
    {
        WaitForSeconds waitSeconds = new WaitForSeconds(_waitSeconds);
        float currentTime = 0;

        while (currentTime <= _loadTime)
        {
            _loadSlider.value = Mathf.Lerp(0, 1, currentTime / _loadTime);
            yield return _waitSeconds;
            currentTime += _waitSeconds;
        }

        Authorized?.Invoke();
    }
}