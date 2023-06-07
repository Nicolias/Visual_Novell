using System;
using System.Collections.Generic;
using XNode;

public class FAQController : IController
{
    public event Action OnComplete;

    private IChoiceModel _model;
    private FAQView _view;
    private FAQCommander _FAQCommander;

    private List<(Node, string)> _question = new();

    public FAQController(IChoiceModel model, FAQView view, FAQCommander fAQCommander)
    {
        _model = model;
        _view = view;
        _FAQCommander = fAQCommander;

        for (int i = 0; i < _model.Choices.Length; i++)
            _question.Add((_model.Nodes[i], _model.Choices[i]));

        _FAQCommander.OnDialogEnd += Execute;
    }

    public void Execute()
    {
        if (_question.Count == 0)
        {
            _FAQCommander.OnDialogEnd -= Execute;
            OnComplete?.Invoke();
            return;
        }

        _view.OnQuestionSelected += OnCallBackView;
        _view.Show(_question);
    }

    private void OnCallBackView((Node, string) question)
    {
        _view.OnQuestionSelected -= OnCallBackView;
        _question.Remove(question);
        _FAQCommander.PackAndExecuteCommand(question.Item1);
    }
}
