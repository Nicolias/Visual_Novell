using System;
using XNode;

public class ChoicesPresentar : IChoisePresenter
{
    public event Action Completed;

    private IChoiceView _view;
    private IChoiceModel _model;

    public ChoicesPresentar(IChoiceModel model, IChoiceView view)
    {
        _model = model;
        _view = view;
    }

    public void Execute()
    {
        _view.OnChoiceMade += OnCallBackView;
        _view.Show(_model);
    }

    private void OnCallBackView(Node node)
    {
        _model.SetEndPort(node);
        _view.OnChoiceMade -= OnCallBackView;
        Completed?.Invoke();
    }
}