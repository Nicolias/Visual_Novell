using System;
using UnityEngine;

public class TimesOfDayServise : MonoBehaviour
{
    private DateTime _currentTime;

    public DateTime CurrentTime => _currentTime;

    private void Update()
    {
        _currentTime = DateTime.Now;
    }

    public TimesOfDayType GetCurrentTimesOfDay()
    {
        switch (_currentTime.Hour)
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
}
