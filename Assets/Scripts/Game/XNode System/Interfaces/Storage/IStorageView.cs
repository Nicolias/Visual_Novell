
using System;

public interface IStorageView
{
    public event Action OnAccureCompleted;

    public void Accure(int value);

    public void Decreese(int value);
}
