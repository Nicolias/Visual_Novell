using UnityEngine;
using XNode;

public class Commander : MonoBehaviour
{
    [SerializeField] private NodeGraph _graph;
    [SerializeField] private SpeechView _speechView;
    //[SerializeField] private BackgroundView _backgroundView;
    //[SerializeField] private ChoicesView _choicesView;
    //[SerializeField] private PortraitView _portraitView;

    private (ICommand Command, Node Node) _curent;

    private void Start()
    {
        _curent = Packing(_graph.nodes[0]);
        _curent.Command.OnComplete += Next;
        _curent.Command.Execute();
    }

    private (ICommand, Node) Packing(Node node)
    {
        (ICommand command, Node node) result;

        result.node = node;

        result.command = node switch
        {
            ISpeechModel speech => new SpeechPresenter(speech, _speechView),
            //IModelBackground background => new BackgroundController(background, _backgroundView),
            //IModelChoices choice => new ChoicesPresentar(choice, _choicesView),
            //IModelPortrait portrait => new PortraitController(portrait, _portraitView),
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