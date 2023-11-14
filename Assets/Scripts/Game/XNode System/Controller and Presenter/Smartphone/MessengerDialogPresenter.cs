using System;
using XNode;

public class MessengerDialogPresenter : IPresentar
{
    public event Action Completed;

    private MessengerDialogSpeechModel _model;
    private ChatView _view;

    private Node _currentNode;

    public MessengerDialogPresenter(MessengerDialogSpeechModel model, ChatView view, Node currentNode, StaticData staticData)
    {
        _model = model;
        _view = view;
        _currentNode = currentNode;
    }

    public void Execute()
    {
        Messege messege = new(_model.Avatar, _model.SpeakerName, _model.Text, _model.MessegeSenderType, _currentNode);
        _view.CreateMessegeView(messege);
        
        Completed?.Invoke();
    }
}