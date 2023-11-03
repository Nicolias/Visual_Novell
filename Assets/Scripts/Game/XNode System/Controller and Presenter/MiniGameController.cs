﻿using System;
using Zenject;

public class MiniGameController : IController
{
    public event Action Complete;

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
        _view.Closed += CallBack;
    }

    private void CallBack()
    {
        if (_battery.ChargeLevel <= _model.BettaryChargeLevelCondition)
        {
            _view.Closed -= CallBack;
            _view.Close();
            Complete?.Invoke();
        }
    }
}