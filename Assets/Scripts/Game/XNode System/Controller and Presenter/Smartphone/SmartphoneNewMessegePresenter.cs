using System;
using XNode;

public class SmartphoneNewMessegePresenter : IPresentar
{
    public event Action Completed;

    private NewDialogInSmartphoneModel _model;
    private Messenger _view;

    public SmartphoneNewMessegePresenter(NewDialogInSmartphoneModel model, Messenger view)
    {
        _model = model;
        _view = view;
    }

    public void Execute()
    {
        _view.AddNewMessege(_model.NewDialog);
        _view.OnChatRed += CallBack;
    }

    private void CallBack(NodeGraph messege)
    {
        if (messege != _model.NewDialog.Messege)
            return;

        _view.OnChatRed -= CallBack;
        Completed?.Invoke();
    }
}
