
public class DialogSpeechPresenter : SpeechPresentar
{
    private readonly IDialogSpeechModel _model;
    private readonly DialogSpeechView _view;

    public DialogSpeechPresenter(IDialogSpeechModel model, DialogSpeechView view)
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