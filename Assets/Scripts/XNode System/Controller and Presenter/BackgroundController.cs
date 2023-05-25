using System;

public class BackgroundController : ICommand
{
    public event Action OnComplete;
    private BackgroundView _view;
    private BackgroundModel _model;

    public BackgroundController(BackgroundModel model, BackgroundView view)
    {
        _model = model;
        _view = view;
    }

    public void Execute()
    {
        _view.OnPicturChange += CallBack;
        _view.Replace(_model.Sprite);
    }

    private void CallBack()
    {
        _view.OnPicturChange -= CallBack;
        OnComplete?.Invoke();
    }
}
