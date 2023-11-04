
public class DialogSpeechPresenter : SpeechPresentar
{
    private IDialogSpeechModel _model;
    private DialogSpeechView _view;

    public DialogSpeechPresenter(IDialogSpeechModel model, DialogSpeechView view, StaticData staticData)
        : base(model, view, staticData)
    {
        _model = model;
        _view = view;
    }

    protected override void ShowSpeech()
    {
        _view.ShowSmooth(_model);
    }

    protected override void ShowSpeechSmooth()
    {
        _view.Show(_model);
    }
}