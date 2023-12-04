using System;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.CloudCode;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Collections;

public class TimesOfDayServise : MonoBehaviour
{
    private WaitForSeconds _waitOneSecond = new WaitForSeconds(1f);

    private DateTime _currentTime = new DateTime(1970, 1, 1, 3, 0, 0);

    public DateTime CurrentTime => _currentTime;

    public event Action<int, int> TimeChanged;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();

        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        var a = await CallGetServerEpochTimeEndpoint();
        _currentTime = _currentTime.AddMilliseconds(a);

        Debug.Log(new DateTime().AddMilliseconds(a));
        Debug.Log(_currentTime);
    }

    private void OnEnable()
    {
        StartCoroutine(Timer());
    }

    private void OnDisable()
    {
        StopCoroutine(Timer());
    }

    public TimesOfDayType GetCurrentTimesOfDay()
    {
        switch (CurrentTime.Hour)
        {
            case (> 18):
                return TimesOfDayType.Evning;
            case (> 12):
                return TimesOfDayType.Day;
            case (> 6):
                return TimesOfDayType.Morning;
            case (> 0):
                return TimesOfDayType.Night;
            default:
                return TimesOfDayType.Night;
        }
    }

    private async Task<long> CallGetServerEpochTimeEndpoint()
    {
        try
        {
            return await CloudCodeService.Instance.CallEndpointAsync<long>(
                "GetServerTime",
                new Dictionary<string, object>());
        }
        catch (Exception e)
        {
            Debug.Log("Problem calling cloud code endpoint: " + e.Message);
            Debug.LogException(e);
        }

        return default;
    }

    private IEnumerator Timer()
    {
        int previousMinute = _currentTime.Minute;

        while (enabled)
        {
            yield return _waitOneSecond;
            _currentTime = _currentTime.AddSeconds(1);
            
            if (_currentTime.Minute != previousMinute)
            {
                previousMinute = _currentTime.Minute;
                TimeChanged?.Invoke(_currentTime.Hour, _currentTime.Minute);
            }
        }
    }
}