using UnityEngine;
using XNode;

public class FAQCommander : Commander
{
    [SerializeField] private DialogSpeechView _dialogSpeechView;
    [SerializeField] private CharacterPortraitView _portraitView;

    protected override (ICommand, Node) Packing(Node node)
    {
        (ICommand command, Node node) result;

        result.node = node;

        result.command = node switch
        {
            DialogSpeechModel dialogSpeech => new SpeechPresentar(dialogSpeech, _dialogSpeechView, StaticData),
            ICharacterPortraitModel portrait => new CharacterPortraitController(portrait, _portraitView),
            _ => null
        };

        return result;
    }
}