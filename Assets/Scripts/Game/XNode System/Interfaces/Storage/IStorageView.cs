using System;

public interface IStorageView
{
    public event Action AccureCompleted;
    public event Action<int> ValueChanged;

    public int CurrentValue { get; }

    public void Accure(int value);

    public void Decreese(int value);
}
