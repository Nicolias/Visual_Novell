public class MonologSpeechPresenter : SpeechPresentar
{
    private readonly IMonologSpeechModel _model;
    private readonly MonologSpeechView _view;

    public MonologSpeechPresenter(IMonologSpeechModel model, MonologSpeechView view) 
        : base(model, view)
    {
        _model = model;
        _view = view;
    }

    protected override void Show()
    {
        _view.Show(_model);
    }

    protected override void ShowSmooth()
    {
        _view.ShowSmooth(_model);
    }
}
