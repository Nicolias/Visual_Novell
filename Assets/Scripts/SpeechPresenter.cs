using System;

public class SpeechPresenter : IPresentar
{
    public event Action OnComplete;

    private ISpeechModel _model;
    private ISpeechView _view;

    public SpeechPresenter(ISpeechModel model, ISpeechView view)
    {
        _model = model;
        _view = view;
    }

    public void Execute()
    {
        _view.OnClick += OnCallBackView;
        _view.ShowSmooth(_model.SpeakerName, _model.Text);
    }

    private void OnCallBackView()
    {
        if (_view.ShowStatus == ShowTextStatus.Showing)
        {
            _view.Show(_model.SpeakerName, _model.Text);
            return;
        }

        _view.OnClick -= OnCallBackView;
        OnComplete?.Invoke();
    }
}
