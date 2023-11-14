using System;
using XNode;

public class SmartphoneCallPresentar : IChoisePresenter
{
    public event Action Completed;

    private ISmartphoneCallView _view;
    private ICallModel _model;

    private AudioServise _audioServise;

    public SmartphoneCallPresentar(ICallModel model, ISmartphoneCallView view, AudioServise audioServise)
    {
        _model = model;
        _view = view;
        _audioServise = audioServise;
    }

    public void Execute()
    {
        _view.OnChoiceMade += OnCallBackView;
        _view.Show(_model);
        _audioServise.CallSound.Play();
    }

    private void OnCallBackView(Node node)
    {
        _model.SetEndPort(node);
        _view.OnChoiceMade -= OnCallBackView;
        Completed?.Invoke();
        _audioServise.CallSound.Stop();
    }
}
