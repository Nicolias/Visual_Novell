
using System;

public interface IStorageView
{
    public event Action AccureCompleted;

    public void Accure(int value);

    public void Decreese(int value);
}
