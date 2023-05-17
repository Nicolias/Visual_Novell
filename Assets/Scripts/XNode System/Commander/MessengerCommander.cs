using System;
using XNode;
using Zenject;

public class MessengerCommander : Commander
{
    [Inject] private ChatView _chatView;
    [Inject] private StaticData _staticData;

    protected override (ICommand, Node) Packing(Node node)
    {
        (ICommand command, Node node) result;

        result.node = node;

        result.command = node switch
        {
            MessengerDialogSpeechModel dialog => new MessengerDialogPresenter(dialog, _chatView, node, _staticData),
            _ => null
        };            

        return result;
    }
}
