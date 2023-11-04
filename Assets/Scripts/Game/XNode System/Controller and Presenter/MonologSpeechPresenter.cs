public class MonologSpeechPresenter : SpeechPresentar
{
    private IMonologSpeechModel _model;
    private MonologSpeechView _view;

    public MonologSpeechPresenter(IMonologSpeechModel model, MonologSpeechView view, StaticData staticData) 
        : base(model, view, staticData)
    {
        _model = model;
        _view = view;
    }

    protected override void ShowSpeech()
    {
        _view.Show(_model);
    }

    protected override void ShowSpeechSmooth()
    {
        _view.Show(_model);
    }
}
