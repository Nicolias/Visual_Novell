using System;
using UnityEngine;

public class Battery : MonoBehaviour, IStorageView
{
    public event Action OnClosed;

    private int _chargeLeve;

    private void Start()
    {
        _chargeLeve = 100;
    }

    public void Accure(int value)
    {
        _chargeLeve += value;

        if (_chargeLeve > 100)
            _chargeLeve = 100;

        OnClosed?.Invoke();
    }

    public void Decreese(int value)
    {
        if (_chargeLeve <= 0) throw new InvalidOperationException();

        _chargeLeve -= value;
    }
}