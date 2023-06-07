using System;
using XNode;

public class NameInputPresenter : IPresentar
{
    public event Action OnComplete;

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
        _view.OnTextInput += OnCallBackView;
    }

    private void OnCallBackView(string newNickname)
    {
        _staticData.SetNickname(newNickname);
        _view.OnTextInput -= OnCallBackView;
        OnComplete?.Invoke();
    }
}