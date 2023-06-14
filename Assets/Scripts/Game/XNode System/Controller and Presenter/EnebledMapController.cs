using System;

public class EnebledMapController : IController
{
    public event Action OnComplete;

    private readonly Map _view;
    private readonly ChangeMapEnabledModel _model;

    public EnebledMapController(Map view, ChangeMapEnabledModel model)
    {
        _view = view;
        _model = model;
    }

    public void Execute()
    {
        _view.SetEnabled(_model.Enebled);
        OnComplete?.Invoke();
    }
}