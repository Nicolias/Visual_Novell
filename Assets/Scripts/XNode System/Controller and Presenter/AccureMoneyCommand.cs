using System;

public class AccureMoneyCommand : ICommand
{
    public event Action OnComplete;

    private AccureMoneyModel _model;
    private AccureMoneyView _view;
    private Smartphone _smarphone;

    public AccureMoneyCommand(AccureMoneyModel model, AccureMoneyView view, Smartphone smartphone)
    {
        _model = model;
        _view = view;
        _smarphone = smartphone;
    }

    public void Execute()
    {
        _smarphone.OnClosed += SmartphoneCloseCallBack;
    }

    private void SmartphoneCloseCallBack()
    {
        _smarphone.OnClosed -= SmartphoneCloseCallBack;

        _view.Show();
        _view.OnComplete += CallBack;
        _view.AccureMoney(_model.Money);
    }

    private void CallBack()
    {
        _view.OnComplete -= CallBack;
        OnComplete?.Invoke();
    }
}
