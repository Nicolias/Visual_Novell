using System;
using XNode;

public class MessengerDialogPresenter : IPresentar
{
    private MessengerDialogSpeechModel _model;
    private IChatWindow _view;

    private Node _currentNode;

    public event Action Complete;

    public MessengerDialogPresenter(MessengerDialogSpeechModel model, IChatWindow view, Node currentNode)
    {
        _model = model;
        _view = view;
        _currentNode = currentNode;
    }

    public void Execute()
    {
        Messege newMessege = new Messege(_model.Avatar, _model.SpeakerName, _model.Text, _model.MessegeSenderType, _currentNode);
        _view.Recieve(newMessege, Complete);
    }
}