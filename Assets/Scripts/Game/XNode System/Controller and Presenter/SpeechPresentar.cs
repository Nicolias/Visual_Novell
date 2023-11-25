using System;

public class SpeechPresentar : IPresentar 
{
    public event Action Completed;

    protected ISpeechModel _model;
    protected SpeechView _view;

    public SpeechPresentar(ISpeechModel model, SpeechView view)
    {
        _model = model;
        _view = view;
    }    

    public void Execute()
    {
        _view.OnClick += OnCallBackView;
        _view.ShowSmooth(_model);

        if (_model.IsImmediatelyNextNode)
        {
            _view.OnClick -= OnCallBackView;
            Completed?.Invoke();
        }
    }

    private void OnCallBackView()
    {
        if (_view.ShowStatus == ShowTextStatus.Showing)
        {
            _view.Show(_model);
        }
        else
        {
            _view.OnClick -= OnCallBackView;
            Completed?.Invoke();
        }
    }
}
