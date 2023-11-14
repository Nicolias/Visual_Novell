using System;

public class BackgroundController : ICommand
{
    public event Action Completed;
    private BackgroundView _view;
    private BackgroundModel _model;
    private CollectionPanel _collectionPanel;

    public BackgroundController(BackgroundModel model, BackgroundView view, CollectionPanel collectionPanel)
    {
        _model = model;
        _view = view;
        _collectionPanel = collectionPanel;
    }

    public void Execute()
    {
        _view.OnPicturChanged += CallBack;
        _view.Replace(_model.Sprite);
        _collectionPanel.HideItems();
    }

    private void CallBack()
    {
        _view.OnPicturChanged -= CallBack;
        Completed?.Invoke();
    }
}
