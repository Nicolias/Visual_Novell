using System;

public class SetTimeOnSmartphoneCommand : ICommand
{
    public event Action Completed;

    private SetTimeOnSmartphoneWatchModel _model;
    private Smartphone _smartphone;

    public SetTimeOnSmartphoneCommand(SetTimeOnSmartphoneWatchModel model, Smartphone smartphone)
    {
        _model = model;
        _smartphone = smartphone;
    }

    public void Execute()
    {
        _smartphone.SetTime(_model.Hour, _model.Minute);
        Completed?.Invoke();
    }
}