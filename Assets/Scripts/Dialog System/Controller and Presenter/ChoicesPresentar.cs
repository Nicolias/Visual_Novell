using System;
using XNode;

public class ChoicesPresentar : IChoicePresentar
{
    public event Action OnComplete;

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
        _view.Show(_model.Choices, _model.Nodes, _model.QuestionText);
    }

    public void OnCallBackView(Node node)
    {
        _model.SetEndPort(node);
        _view.OnChoiceMade -= OnCallBackView;
        OnComplete?.Invoke();
    }
}
