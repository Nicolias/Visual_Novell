using System;

public class EnebledController : IController
{
    public event Action Complete;

    private readonly Smartphone _view;
    private readonly ChangeEnabledModel _model;

    public EnebledController(Smartphone view, ChangeEnabledModel model)
    {
        _view = view;
        _model = model;
    }

    public void Execute()
    {
        _view.ChangeEnabled(_model.Windows);

        Complete?.Invoke();
    }
}
