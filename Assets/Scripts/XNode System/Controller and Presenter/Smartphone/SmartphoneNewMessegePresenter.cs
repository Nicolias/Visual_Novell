using System;

public class SmartphoneNewMessegePresenter : IPresentar
{
    public event Action OnComplete;

    private NewDialogInSmartphoneModel _model;
    private Smartphone _view;

    public SmartphoneNewMessegePresenter(NewDialogInSmartphoneModel model, Smartphone view)
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