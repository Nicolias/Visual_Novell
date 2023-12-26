using System;

public class EnebledController : IController
{
    public event Action Completed;

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

        if (_model.IsShowGuid)
            for (int i = 0; i < _model.Windows.Count; i++)
                _view.ShowGuid( _model.Windows.GetKey(i));

        Completed?.Invoke();
    }
}
