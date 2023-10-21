using System;

public class RequirementOpenDUXCommand : ICommand
{
    public event Action Complete;

    private DUXWindow _duxWindow;
    private Smartphone _smartphone;

    public RequirementOpenDUXCommand(Smartphone smartphone, DUXWindow duxWindow)
    {
        _smartphone = smartphone;
        _duxWindow = duxWindow;
    }

    public void Execute()
    {
        _duxWindow.OnClosed += DuxCloseCallBack;
        _smartphone.OpenAccesToDUX();
    }

    private void DuxCloseCallBack()
    {
        _duxWindow.OnClosed -= DuxCloseCallBack;
        _smartphone.Closed += SmartphoneCloseCallBack;
    }

    private void SmartphoneCloseCallBack()
    {
        _smartphone.Closed -= SmartphoneCloseCallBack;
        Complete?.Invoke();
    }
}