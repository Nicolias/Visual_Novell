using System;

public class SpeechPresentar : IPresentar 
{
    public event Action OnComplete;

    private ISpeechModel _model;
    private ISpeechView _view;
    private StaticData _staticData;

    public SpeechPresentar(DialogSpeechModel model, DialogSpeechView view, StaticData staticData)
    {
        _model = model;
        _view = view;
        _staticData = staticData;
    }

    public SpeechPresentar(MonologSpeechModel model, MonologSpeechView view, StaticData staticData)
    {
        _model = model;
        _view = view;
        _staticData = staticData;
    }

    public void Execute()
    {
        _model.TryReplaceNickname(_staticData.SpecWordForNickName, _staticData.Nickname);
        _view.OnClick += OnCallBackView;
        _view.ShowSmooth(_model);

        if (_model.IsImmediatelyNextNode)
            OnComplete?.Invoke();
    }

    private void OnCallBackView()
    {
        if (_view.ShowStatus == ShowTextStatus.Showing)
        {
            _view.Show(_model);
            return;
        }

        _view.OnClick -= OnCallBackView;
        OnComplete?.Invoke();
    }
}
