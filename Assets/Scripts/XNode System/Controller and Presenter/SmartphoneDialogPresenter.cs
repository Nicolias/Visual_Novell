using System;

public class SmartphoneDialogPresenter : IPresentar
{
    public event Action OnComplete;

    private NewDialogInSmartphoneModel _model;
    private Smartphone _view;

    public SmartphoneDialogPresenter(NewDialogInSmartphoneModel model, Smartphone view)
    {
        _model = model;
        _view = view;
    }

    public void Execute()
    {
        _view.AddNewMessege(_model.NewDialog);
        OnComplete?.Invoke();
    }
}