using System;

public class AccureStoregeValueCommand : ICommand
{
    public event Action Complete;

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
        if (_smarphone.SelfCanvas.enabled)
            _smarphone.Closed += SmartphoneCloseCallBack;
        else
            SmartphoneCloseCallBack();
    }

    private void SmartphoneCloseCallBack()
    {
        _smarphone.Closed -= SmartphoneCloseCallBack;

        _view.OnAccureCompleted += CallBack;
        _view.Accure(_model.Value);
    }

    private void CallBack()
    {
        _view.OnAccureCompleted -= CallBack;
        Complete?.Invoke();
    }
}
