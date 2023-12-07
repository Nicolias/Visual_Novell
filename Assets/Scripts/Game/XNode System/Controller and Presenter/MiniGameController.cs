﻿using System;
using Zenject;

public class MiniGameController : IController
{
    public event Action Completed;

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
        _view.Enter(_model.CharacterType);
        _view.GameEnded += CallBack;
    }

    private void CallBack()
    {
        if (_battery.CurrentValue <= _model.BettaryChargeLevelCondition)
        {
            _view.GameEnded -= CallBack;
            _view.Close();
            Completed?.Invoke();
        }
    }
}