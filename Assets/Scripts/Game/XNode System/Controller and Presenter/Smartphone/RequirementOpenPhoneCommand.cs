﻿using System;

public class RequirementOpenPhoneCommand : ICommand
{
    public event Action OnComplete;
    private Smartphone _smartphone;

    public RequirementOpenPhoneCommand(Smartphone smartphone)
    {
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
