using System;
using System.Collections.Generic;
using XNode;

public class SmartphoneNewMessegePresenter : IPresentar
{
    public event Action Completed;

    private NewDialogInSmartphoneModel _model;
    private Messenger _view;

    private List<MessegeData> _dialogsForPass;

    public SmartphoneNewMessegePresenter(NewDialogInSmartphoneModel model, Messenger view)
    {
        _model = model;
        _view = view;
    }

    public void Execute()
    {
        _dialogsForPass = new List<MessegeData>(_model.NewDialogs);

        foreach (var newDialog in _model.NewDialogs)
            _view.AddNewMessage(newDialog);

        _view.ChatRead += CallBack;
    }

    private void CallBack(NodeGraph messege)
    {
        if (_dialogsForPass.Exists(dialog => dialog.Messege == messege))
            _dialogsForPass.RemoveAll(dialogForDelete => dialogForDelete.Messege == messege);

        if (_dialogsForPass.Count > 0)
            return;

        _view.ChatRead -= CallBack;
        Completed?.Invoke();
    }
}
