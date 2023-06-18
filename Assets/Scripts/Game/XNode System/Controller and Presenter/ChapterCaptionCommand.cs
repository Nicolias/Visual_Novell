using System;

public class ChapterCaptionCommand : ICommand
{
    public event Action OnComplete;

    private readonly ChapterCaption _view;
    private readonly ChapterCaptionModel _model;

    public ChapterCaptionCommand(ChapterCaption view, ChapterCaptionModel model)
    {
        _view = view;
        _model = model;
    }

    public void Execute()
    {
        _view.ShowText(_model.ChapterText);
        _view.OnCaptionShowed += OnCaptionShowed;
    }

    private void OnCaptionShowed()
    {
        _view.OnCaptionShowed -= OnCaptionShowed;
        OnComplete?.Invoke();
    }
}