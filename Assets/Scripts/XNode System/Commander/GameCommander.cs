using UnityEngine;
using XNode;

public class GameCommander : Commander
{
    [SerializeField] private StaticData _staticData;

    [SerializeField] private MonologSpeechView _monologSpeechView;
    [SerializeField] private DialogSpeechView _dialogSpeechView;

    [SerializeField] private NameInputView _nameInputView;

    [SerializeField] private ChoiceView _choicesView;

    [SerializeField] private CharacterPortraitView _portraitView;

    [SerializeField] private BackgroundView _backgroundView;

    [SerializeField] private AudioServise _audioServise;

    [SerializeField] private SmartphoneGuideView _smartphoneGuideView;
    [SerializeField] private Smartphone _smartPhone;

    protected override (ICommand, Node) Packing(Node node)
    {
        (ICommand command, Node node) result;

        result.node = node;

        result.command = node switch
        {
            MonologSpeechModel speech => new SpeechPresentar(speech, _monologSpeechView, _staticData),
            DialogSpeechModel dialogSpeech => new SpeechPresentar(dialogSpeech, _dialogSpeechView, _staticData),
            IChoiceModel choice => new ChoicesPresentar(choice, _choicesView),
            BackgroundModel background => new BackgroundController(background, _backgroundView),
            ICharacterPortraitModel portrait => new CharacterPortraitController(portrait, _portraitView),
            AudioModel audio => new AudioController(audio, _audioServise),
            INicknameInputModel => new NameInputPresenter(_nameInputView, _staticData),
            NewDialogInSmartphoneModel newMassegemodel => new SmartphoneNewMessegePresenter(newMassegemodel, _smartPhone),
            SmartphoneGuidModel => new SmartPhoneGuidPresenter(_smartphoneGuideView),
            _ => null
        };

        return result;
    }
}
