using System;
using XNode;

public class NameInputPresenter : IPresentar
{
    public event Action OnComplete;

    private StaticData _staticData;
    private ITextInputView _view;
    private INicknameInputModel _model;

    public NameInputPresenter(ITextInputView nameInputView, INicknameInputModel nicknameInputModel, StaticData staticData)
    {
        _staticData = staticData;
        _view = nameInputView;
        _model = nicknameInputModel;
    }

    public void Execute()
    {
        _view.Show();
        _view.OnTextInput += OnCallBackView;
        _view.OnChoiceMade += OnCallBackView;
    }

    private void OnCallBackView(string newNickname)
    {
        _staticData.SetNickname(newNickname);
    }

    public void OnCallBackView(Node node)
    {
        _model.SetEndPort(node);
        _view.OnChoiceMade -= OnCallBackView;
        OnComplete?.Invoke();
    }
}