using System;

public class AccureStoregeValueCommand : ICommand
{
    public event Action OnComplete;

    private IStorageModel _model;
    private IStorageView _view;
    private Smartphone _smarphone;

    public AccureStoregeValueCommand(IStorageModel model, IStorageView view, Smartphone smartphone)
    {
        _model = model;
        _view = view;
        _smarphone = smartphone;
    }

    public void Execute()
    {
        if (_smarphone.gameObject.activeInHierarchy)
            _smarphone.OnClosed += SmartphoneCloseCallBack;
        else
            SmartphoneCloseCallBack();
    }

    private void SmartphoneCloseCallBack()
    {
        _smarphone.OnClosed -= SmartphoneCloseCallBack;

        _view.OnClosed += CallBack;
        _view.Accure(_model.Value);
    }

    private void CallBack()
    {
        _view.OnClosed -= CallBack;
        OnComplete?.Invoke();
    }
}
