using UnityEngine;
using XNode;
using Zenject;

public class MessengerCommander : Commander
{    
    [Inject] private ChatView _chatView;

    protected override (ICommand, Node) Packing(Node node)
    {
        (ICommand command, Node node) result;

        result.node = node;

        result.command = node switch
        {
            MessengerDialogSpeechModel dialog => new MessengerDialogPresenter(dialog, _chatView, node),
            _ => null
        };

        return result;
    }
}
