using System;
using XNode;

public class SmartphoneNewMessegePresenter : IPresentar
{
    public event Action Complete;

    private NewDialogInSmartphoneModel _model;
    private Messenger _view;

    public SmartphoneNewMessegePresenter(NewDialogInSmartphoneModel model, Messenger view)
    {
        _model = model;
        _view = view;
    }

    public void Execute()
    {
        _view.AddNewMessage(_model.NewDialog);
        _view.ChatRead += CallBack;
    }

    private void CallBack(NodeGraph messege)
    {
        if (messege != _model.NewDialog.Messege)
            return;

        _view.ChatRead -= CallBack;
        Complete?.Invoke();
    }
}
