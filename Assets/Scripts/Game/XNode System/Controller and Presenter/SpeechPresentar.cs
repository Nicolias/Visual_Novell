using System;

public abstract class SpeechPresentar : IPresentar
{
    private readonly ISpeechModel _model;
    private readonly SpeechView _view;

    public event Action Completed;

    public SpeechPresentar(ISpeechModel model, SpeechView view)
    {
        _model = model;
        _view = view;
    }

    public void Execute()
    {
        _view.Clicked += OnCallBackView;
        ShowSmooth();

        if (_model.IsImmediatelyNextNode)
        {
            _view.Clicked -= OnCallBackView;
            Completed?.Invoke();
        }
    }

    protected abstract void Show();

    protected abstract void ShowSmooth();

    private void OnCallBackView()
    {
        if (_view.ShowStatus == ShowTextStatus.Showing)
        {
            Show();
        }
        else
        {
            _view.Clicked -= OnCallBackView;
            Completed?.Invoke();
        }
    }
}