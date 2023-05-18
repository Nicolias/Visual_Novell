using System;

public class RequirementOpenPhoneCommand : ICommand
{
    public event Action OnComplete;

    private CoroutineServise _coroutineServise;
    private Smartphone _smartphone;

    public RequirementOpenPhoneCommand(CoroutineServise coroutineServise, Smartphone smartphone)
    {
        _coroutineServise = coroutineServise;
        _smartphone = smartphone;
    }

    public void Execute()
    {
        _smartphone.OnClosed += CallBack;
    }

    private void CallBack()
    {
        _smartphone.OnClosed -= CallBack;
        OnComplete?.Invoke();
    }
}