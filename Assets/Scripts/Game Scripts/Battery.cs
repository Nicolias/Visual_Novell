using System;
using UnityEngine;

public class Battery : MonoBehaviour, IStorageView
{
    public event Action OnAccureCompleted;

    private int _chargeLeve;

    public int ChargeLevel => _chargeLeve;

    private void Start()
    {
        _chargeLeve = 100;
    }

    public void Accure(int value)
    {
        _chargeLeve += value;

        if (_chargeLeve > 100)
            _chargeLeve = 100;

        OnAccureCompleted?.Invoke();
    }

    public void Decreese(int value)
    {
        if (_chargeLeve <= 0) throw new InvalidOperationException();

        _chargeLeve -= value;
    }
}