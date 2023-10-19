using System;

public interface ICommand
{
    public event Action Complete;

    public void Execute();
}
