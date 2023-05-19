using UnityEngine;
using XNode;

public class FAQCommander : Commander
{
    [SerializeField] private DialogSpeechView _dialogSpeechView;

    protected override (ICommand, Node) Packing(Node node)
    {
        (ICommand command, Node node) result;

        result.node = node;

        result.command = node switch
        {
            DialogSpeechModel dialogSpeech => new SpeechPresentar(dialogSpeech, _dialogSpeechView, StaticData),
            _ => null
        };

        return result;
    }
}