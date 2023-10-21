using System;

public class RequirementOpenPhoneCommand : ICommand
{
    public event Action Complete;
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
        Complete?.Invoke();
    }
}
