using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Battery : MonoBehaviour, IStorageView, ISaveLoadObject
{
    [Inject] private SaveLoadServise _saveLoadServise;

    private bool _isTimer;
    private int _chargeLeve;
    private int _maxCharge = 100;

    public int CurrentValue => _chargeLeve;
    public int MaxValue => _maxCharge;

    private int _rechargeByMinute = 8;

    private const string _saveKey = "BatterySave";

    public event Action AccureCompleted;
    public event Action<int> ValueChanged;

    private void OnEnable()
    {
        Add();

        if (_saveLoadServise.HasSave(_saveKey))
            Load();
        else
            _chargeLeve = _maxCharge;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Accure(int value)
    {
        _chargeLeve += value;

        AccureCompleted?.Invoke();
        ValueChanged?.Invoke(_chargeLeve);
    }

    public void Decreese(int value)
    {
        if (_chargeLeve - value < 0) throw new InvalidOperationException();

        _chargeLeve -= value;

        if (_isTimer == false)
            StartCoroutine(Timer(new(0, _rechargeByMinute, 0)));

        ValueChanged?.Invoke(_chargeLeve);
    }

    private IEnumerator Timer(TimeSpan leftTime)
    {
        _isTimer = true;

        while (_chargeLeve < _maxCharge)
        {
            while (leftTime != new TimeSpan(0, 0, 0))
            {
                Debug.Log(leftTime);

                yield return new WaitForSeconds(1f);

                leftTime -= new TimeSpan(0, 0, 1);

                if (_chargeLeve >= _maxCharge)
                    StopAllCoroutines();
            }

            Accure(1);
        }

        _isTimer = false;
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.BatterySaveData()
        {
            ChargeLevel = _chargeLeve,
            MaxChargeLevel = _maxCharge,

            LastOpenedYear = DateTime.Now.Year,
            LastOpenedMonths = DateTime.Now.Month,
            LastOpenedDay = DateTime.Now.Day,
            LastOpenedHour = DateTime.Now.Hour,
            LastOpenedMinute = DateTime.Now.Minute,
        });
    }

    public void Enchance(int value)
    {
        if (value <= 0)
            throw new InvalidOperationException("Попытка уменьшить максимальный заряд");

        _maxCharge += value;

        ValueChanged?.Invoke(_chargeLeve);
    }

    public void Load()
    {
        var data = _saveLoadServise.Load<SaveData.BatterySaveData>(_saveKey);
        _chargeLeve = data.ChargeLevel;
        _maxCharge = data.MaxChargeLevel;

        DateTime lastOpenedTime = new(data.LastOpenedYear, 
            data.LastOpenedMonths, 
            data.LastOpenedDay, 
            data.LastOpenedHour, 
            data.LastOpenedMinute, 
            0);

        TimeSpan timeSinceLastOpened = DateTime.Now - lastOpenedTime;

        if (timeSinceLastOpened.TotalSeconds < 0)
            Application.Quit();

        double minuts = timeSinceLastOpened.TotalMinutes;

        while (_chargeLeve < _maxCharge)
        {
            if (minuts < _rechargeByMinute)
                break;

            minuts -= _rechargeByMinute;
            _chargeLeve++;
        }

        ValueChanged?.Invoke(_chargeLeve);

        if (_chargeLeve < _maxCharge && _isTimer == false)
            StartCoroutine(Timer(new(0,8,0)));
    }

    public void Add()
    {
        _saveLoadServise.Add(this);
    }
}
