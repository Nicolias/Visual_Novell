using System;

public class AudioController : IController
{
    public event Action OnComplete;

    private AudioServise _view;
    private AudioModel _model;

    public AudioController(AudioModel model, AudioServise view)
    {
        _model = model;
        _view = view;
    }

    public void Execute()
    {
        _view.PlaySound(_model.AudioClip);
        OnComplete?.Invoke();
    }
}
