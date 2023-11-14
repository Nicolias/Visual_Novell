﻿using System;
using XNode;

public class NameInputPresenter : IPresentar
{
    public event Action Completed;

    private StaticData _staticData;
    private ITextInputView _view;

    public NameInputPresenter(ITextInputView nameInputView, StaticData staticData)
    {
        _staticData = staticData;
        _view = nameInputView;
    }

    public void Execute()
    {
        _view.Show();
        _view.TextInput += OnCallBackView;
    }

    private void OnCallBackView(string newNickname)
    {
        _staticData.SetNickname(newNickname);
        _view.TextInput -= OnCallBackView;
        Completed?.Invoke();
    }
}