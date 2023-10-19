﻿using System;

public class DecreeseStoregeValueCommand : ICommand
{
    public event Action Complete;

    private IStorageModel _model;
    private IStorageView _view;

    public DecreeseStoregeValueCommand(IStorageModel model, IStorageView view)
    {
        _model = model;
        _view = view;
    }

    public void Execute()
    {
        _view.Decreese(_model.Value);
        Complete?.Invoke();
    }
}
