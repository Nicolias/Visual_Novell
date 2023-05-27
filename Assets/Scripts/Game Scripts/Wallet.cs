using System;
using UnityEngine;

public class Wallet : MonoBehaviour, IStorageView
{
    public event Action OnClosed;

    [SerializeField] private AccureMoneyPanel _accureMoneyPanel;

    private int _amountMoney;

    private void OnEnable()
    {
        _accureMoneyPanel.OnClosed += CallBack;
    }

    private void OnDisable()
    {
        _accureMoneyPanel.OnClosed -= CallBack;
    }

    public void Accure(int money)
    {
        if (money <= 0) throw new InvalidOperationException("Начисленно 0 денег");
        _accureMoneyPanel.PrintAccureMoney(money);

        _amountMoney += money;
    }

    public void Decreese(int money)
    {
        if (money <= 0) throw new InvalidOperationException();
        if (_amountMoney <= 0) throw new InvalidOperationException("Недостаточно средств");

        _amountMoney -= money;
    }

    private void CallBack()
    {
        OnClosed?.Invoke();
    }
}
