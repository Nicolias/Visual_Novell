using System;
using UnityEngine;
using Zenject;

public class Wallet : MonoBehaviour, IStorageView, ISaveLoadObject
{
    [Inject] private SaveLoadServise _saveLoadServise;

    [SerializeField] private AccureMoneyPanel _accureMoneyPanel;

    [SerializeField] private int _amountMoney;

    private string _saveKey = "WalletSave";

    public int MoneyCount => _amountMoney;

    public event Action AccureCompleted;
    public event Action<int> MoneyChanged;

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

        MoneyChanged?.Invoke(_amountMoney);
    }

    public void Decreese(int money)
    {
        if (money <= 0) throw new InvalidOperationException();
        if (_amountMoney <= 0) throw new InvalidOperationException("Недостаточно средств");

        _amountMoney -= money;

        MoneyChanged?.Invoke(_amountMoney);
    }

    private void CallBack()
    {
        AccureCompleted?.Invoke();
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
