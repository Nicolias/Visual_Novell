using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _amountMoney;

    public void AccureMoney(int money)
    {
        if (money <= 0) throw new InvalidOperationException();

        _amountMoney += money;
    }

    public void DecreeseMoney(int money)
    {
        if (money >= 0) throw new InvalidOperationException();
        if (_amountMoney <= 0) throw new InvalidOperationException("Недостаточно средств");

        _amountMoney -= money;
    }
}
