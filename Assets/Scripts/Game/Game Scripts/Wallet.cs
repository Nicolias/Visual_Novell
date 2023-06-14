using System;
using UnityEngine;
using Zenject;

public class Wallet : MonoBehaviour, IStorageView, ISaveLoadObject
{
    public event Action OnAccureCompleted;

    [Inject] private SaveLoadServise _saveLoadServise;

    [SerializeField] private AccureMoneyPanel _accureMoneyPanel;

    private int _amountMoney;

    private const string _saveKey = "WalletSave";

    private void OnEnable()
    {
        _accureMoneyPanel.OnClosed += CallBack;

        if (_saveLoadServise.HasSave(_saveKey))
            Load();
    }

    private void OnDisable()
    {
        _accureMoneyPanel.OnClosed -= CallBack;

        Save();
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
        OnAccureCompleted?.Invoke();
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.IntData() { Int = _amountMoney });
    }

    public void Load()
    {
        _amountMoney = _saveLoadServise.Load<SaveData.IntData>(_saveKey).Int;
    }
}
