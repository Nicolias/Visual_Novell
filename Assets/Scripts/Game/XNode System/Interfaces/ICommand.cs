using System;

public interface ICommand
{
    public event Action Completed;

    public void Execute();
}
