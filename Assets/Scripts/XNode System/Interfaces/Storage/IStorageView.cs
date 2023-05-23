
using System;

public interface IStorageView
{
    public event Action OnClosed;

    public void Accure(int value);

    public void Decreese(int value);
}
