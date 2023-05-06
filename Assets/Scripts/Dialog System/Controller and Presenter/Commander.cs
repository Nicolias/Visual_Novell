using UnityEngine;
using XNode;

public class Commander : MonoBehaviour
{
    [SerializeField] private NodeGraph _graph;
    [SerializeField] private SpeechView _speechView;
    //[SerializeField] private BackgroundView _backgroundView;
    [SerializeField] private ChoiceView _choicesView;
    [SerializeField] private CharacterPortraitView _portraitView;

    private (ICommand Command, Node Node) _curent;

    private void Start()
    {
        _curent = Packing(_graph.nodes[0]);
        _curent.Command.OnComplete += Next;
        _curent.Command.Execute();
    }

    private void OnDisable()
    {
        _curent.Command.OnComplete -= Next;
    }

    private (ICommand, Node) Packing(Node node)
    {
        (ICommand command, Node node) result;

        result.node = node;

        result.command = node switch
        {
            ISpeechModel speech => new SpeechPresentar(speech, _speechView),
            //IModelBackground background => new BackgroundController(background, _backgroundView),
            IChoiceModel choice => new ChoicesPresentar(choice, _choicesView),
            ICharacterPortraitModel portrait => new CharacterPortraitController(portrait, _portraitView),
            _ => null
        };

        return result;
    }

    private void Next()
    {
        _curent.Command.OnComplete -= Next;
        NodePort port = _curent.Node.GetPort("_outPut").Connection;

        if (port == null) return;

        _curent = Packing(port.node);
        _curent.Command.OnComplete += Next;
        _curent.Command.Execute();
    }
}