using System;

public class ChangeLocationController : ICommand
{
    public event Action Complete;

    private Map _view;
    private ChangeLocationModel _model;
    private BackgroundView _backgroundView;

    public ChangeLocationController(ChangeLocationModel model, Map view, BackgroundView backgroundView)
    {
        _model = model;
        _view = view;
        _backgroundView = backgroundView; 
    }

    public void Execute()
    {
        _backgroundView.OnPicturChanged += CallBack;
        _view.ChangeLocation(_model.Location);
    }

    private void CallBack()
    {
        _backgroundView.OnPicturChanged -= CallBack;
        Complete?.Invoke();
    }
}
