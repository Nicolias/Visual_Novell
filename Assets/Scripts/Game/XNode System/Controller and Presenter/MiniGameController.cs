using System;
using Zenject;

public class MiniGameController : IController
{
    public event Action OnComplete;

    [Inject] private Battery _battery;

    private MiniGameModel _model;
    private MiniGameSelector _view;

    public MiniGameController(MiniGameModel model, MiniGameSelector view)
    {
        _model = model;
        _view = view;
    }

    public void Execute()
    {
        _view.ShowMiniGameSelectoin(_model.CharacterType);
        _view.OnGameEnded += CallBack;
    }

    private void CallBack()
    {
        if (_battery.ChargeLevel <= _model.BettaryChargeLevelCondition)
        {
            _view.OnGameEnded -= CallBack;
            _view.CloseMiniGamesSelection();
            OnComplete?.Invoke();
        }
    }
}