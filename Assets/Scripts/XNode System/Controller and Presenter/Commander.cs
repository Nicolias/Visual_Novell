using System.Collections.Generic;
using UnityEngine;
using XNode;

public class Commander : MonoBehaviour
{
    [SerializeField] private StaticData _staticData;

    [SerializeField] private NodeGraph _graph;

    [SerializeField] private MonologSpeechView _monologSpeechView;
    [SerializeField] private DialogSpeechView _dialogSpeechView;

    [SerializeField] private NameInputView _nameInputView;

    [SerializeField] private ChoiceView _choicesView;

    [SerializeField] private CharacterPortraitView _portraitView;

    [SerializeField] private BackgroundView _backgroundView;

    [SerializeField] private AudioServise _audioServise;

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
            BackgroundModel background => new BackgroundController(background, _backgroundView),
            MonologSpeechModel speech => new SpeechPresentar(speech, _monologSpeechView, _staticData),
            DialogSpeechModel dialogSpeech => new SpeechPresentar(dialogSpeech, _dialogSpeechView, _staticData),
            AudioModel audio => new AudioController(audio, _audioServise),
            INicknameInputModel => new NameInputPresenter(_nameInputView, _staticData),
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

