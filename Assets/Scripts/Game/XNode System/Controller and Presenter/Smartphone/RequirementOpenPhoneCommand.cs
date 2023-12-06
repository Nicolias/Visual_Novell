using System;

public class RequirementOpenPhoneCommand : ICommand
{
    public event Action Completed;
    private Smartphone _smartphone;

    public RequirementOpenPhoneCommand(Smartphone smartphone)
    {
        _smartphone = smartphone;
    }

    public void Execute()
    {
        _smartphone.Closed += CallBack;
    }

    private void CallBack()
    {
        _smartphone.Closed -= CallBack;
        Completed?.Invoke();
    }
}
