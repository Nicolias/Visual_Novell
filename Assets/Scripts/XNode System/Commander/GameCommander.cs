using UnityEngine;
using XNode;

public class GameCommander : Commander
{
    [SerializeField] private MonologSpeechView _monologSpeechView;
    [SerializeField] private DialogSpeechView _dialogSpeechView;

    [SerializeField] private NameInputView _nameInputView;

    [SerializeField] private ChoiceView _choicesView;

    [SerializeField] private CharacterPortraitView _portraitView;

    [SerializeField] private BackgroundView _backgroundView;

    [SerializeField] private AudioServise _audioServise;

    [SerializeField] private SmartphoneGuideView _smartphoneGuideView;
    [SerializeField] private AccureMoneyView _accureMoneyPanel;

    [SerializeField] private Smartphone _smartPhone;
    [SerializeField] private SmartphoneCallView _callView;

    [SerializeField] private DUXWindow _duxWindow;

    [SerializeField] private FAQView _faqView;
    [SerializeField] private FAQCommander _faqCommander;

    [SerializeField] private CharactersLibrary _characterLibrary;

    protected override (ICommand, Node) Packing(Node node)
    {
        (ICommand command, Node node) result;

        result.node = node;

        result.command = node switch
        {
            MonologSpeechModel speech => new SpeechPresentar(speech, _monologSpeechView, StaticData),
            DialogSpeechModel dialogSpeech => new SpeechPresentar(dialogSpeech, _dialogSpeechView, StaticData),
            FAQModel faqModel => new FAQController(faqModel, _faqView, _faqCommander), 
            IChoiceModel choice => new ChoicesPresentar(choice, _choicesView),
            BackgroundModel background => new BackgroundController(background, _backgroundView),
            ICharacterPortraitModel portrait => new CharacterPortraitController(portrait, _portraitView),
            AudioModel audio => new AudioController(audio, _audioServise),
            INicknameInputModel => new NameInputPresenter(_nameInputView, StaticData),
            NewDialogInSmartphoneModel newMassegemodel => new SmartphoneNewMessegePresenter(newMassegemodel, _smartPhone),
            SmartphoneGuidModel => new SmartPhoneGuidPresenter(_smartphoneGuideView),
            ICallModel callModel => new SmartphoneCallPresentar(callModel, _callView, AudioServise), 
            WaitForSecondsModel waitModel => new WaitForSecondsPresenter(CoroutineServise, waitModel),
            SetTimeOnSmartphoneWatchModel timeModel => new SetTimeOnSmartphoneCommand(timeModel, _smartPhone),
            RequirementOpenPhoneModel => new RequirementOpenPhoneCommand(_smartPhone),
            RequirementOpenDUXModel => new RequirementOpenDUXCommand(_smartPhone, _duxWindow),
            AddSympathyModel sympathyModel => new AddSympthyToCharacterController(sympathyModel, _characterLibrary),
            AccureMoneyModel accureMoneyModel => new AccureMoneyCommand(accureMoneyModel, _accureMoneyPanel, _smartPhone),
            _ => null
        };

        return result;
    }
}
