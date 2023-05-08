using System;

public interface ICommand
{
    public event Action OnComplete;

    public void Execute();
}
