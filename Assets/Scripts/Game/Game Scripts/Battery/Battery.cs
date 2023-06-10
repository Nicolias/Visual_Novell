using System;
using UnityEngine;

public class Battery : MonoBehaviour, IStorageView
{
    public event Action OnAccureCompleted;
    public event Action<int> OnValueChanged;

    private int _chargeLeve;

    public int ChargeLevel => _chargeLeve;

    private void Awake()
    {
        _chargeLeve = 100;
    }

    public void Accure(int value)
    {
        _chargeLeve += value;

        if (_chargeLeve > 100)
            _chargeLeve = 100;

        OnAccureCompleted?.Invoke();
        OnValueChanged?.Invoke(_chargeLeve);
    }

    public void Decreese(int value)
    {
        if (_chargeLeve - value < 0) throw new InvalidOperationException();

        _chargeLeve -= value;

        OnValueChanged?.Invoke(_chargeLeve);
    }
}