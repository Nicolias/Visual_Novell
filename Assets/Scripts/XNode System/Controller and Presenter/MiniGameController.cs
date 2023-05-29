using System;
using Zenject;

public class MiniGameController : IController
{
    public event Action OnComplete;

    [Inject] private Battery _battery;

    private MiniGameModel _model;
    private RockPaperScissors _view;

    public MiniGameController(MiniGameModel model, RockPaperScissors view)
    {
        _model = model;
        _view = view;
    }

    public void Execute()
    {
        _view.StartGame(_model.CharacterType);
        _view.OnGameEnded += CallBack;
    }

    private void CallBack()
    {
        if (_battery.ChargeLevel <= _model.BettaryChargeLevelCondition)
        {
            _view.OnGameEnded -= CallBack;
            _view.EndGame();
            OnComplete?.Invoke();
        }
        else
        {
            _view.StartGame(_model.CharacterType);
        }
    }
}