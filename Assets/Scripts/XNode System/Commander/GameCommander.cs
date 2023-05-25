using UnityEngine;
using XNode;
using Zenject;

public class GameCommander : Commander, ICommanderVisitor
{
    [Inject] private readonly Wallet _wallet;
    [Inject] private readonly Battery _battery;

    [SerializeField] private NodeGraph _currentGraph;

    [SerializeField] private MonologSpeechView _monologSpeechView;
    [SerializeField] private DialogSpeechView _dialogSpeechView;

    [SerializeField] private NameInputView _nameInputView;

    [SerializeField] private ChoiceView _choicesView;

    [SerializeField] private CharacterPortraitView _portraitView;

    [SerializeField] private BackgroundView _backgroundView;

    [SerializeField] private AudioServise _audioServise;

    [SerializeField] private SmartphoneGuideView _smartphoneGuideView;    

    [SerializeField] private Smartphone _smartPhone;
    [SerializeField] private SmartphoneCallView _callView;

    [SerializeField] private DUXWindow _duxWindow;

    [SerializeField] private FAQView _faqView;
    [SerializeField] private FAQCommander _faqCommander;

    [SerializeField] private CharactersLibrary _characterLibrary;

    private (ICommand command, Node node) _result; 

    private void Start()
    {
        if (_currentGraph == null)
            return;

        PackAndExecuteCommand(_currentGraph.nodes[0]);
    }

    protected override (ICommand, Node) Packing(Node node)
    {
        _result.node = node;
        (node as XnodeModel).Accept(this);

        return _result;
    }

    public void Visit(DialogSpeechModel dialogSpeech)
    {
        dialogSpeech.Initialize(StaticData);
        _result.command = new SpeechPresentar(dialogSpeech, _dialogSpeechView, StaticData);
    }
    public void Visit(MonologSpeechModel speech)
    {
        speech.Initialize(StaticData);
        _result.command = new SpeechPresentar(speech, _monologSpeechView, StaticData);
    }

    public void Visit(ICharacterPortraitModel portrait)
    {
        _result.command = new CharacterPortraitController(portrait, _portraitView);
    }
    public void Visit(BackgroundModel background)
    {
        _result.command = new BackgroundController(background, _backgroundView);
    }
    public void Visit(IChoiceModel choice)
    {
        _result.command = new ChoicesPresentar(choice, _choicesView);
    }
    public void Visit(AudioModel audio)
    {
        _result.command = new AudioController(audio, _audioServise);
    }

    public void Visit(INicknameInputModel nickNameModel)
    {
        _result.command = new NameInputPresenter(_nameInputView, StaticData);
    }

    public void Visit(NewDialogInSmartphoneModel newMassegemodel)
    {
        _result.command = new SmartphoneNewMessegePresenter(newMassegemodel, _smartPhone.Messenger);
    }
    public void Visit(SetTimeOnSmartphoneWatchModel timeModel)
    {
        _result.command = new SetTimeOnSmartphoneCommand(timeModel, _smartPhone);
    }
    public void Visit(ICallModel callModel)
    {
        _result.command = new SmartphoneCallPresentar(callModel, _callView, AudioServise);
    }

    public void Visit(SmartphoneGuidModel smartPhoneGuid)
    {
        _result.command = new SmartPhoneGuidPresenter(_smartphoneGuideView);
    }
    public void Visit(WaitForSecondsModel waitModel)
    {
        _result.command = new WaitForSecondsPresenter(CoroutineServise, waitModel);
    }

    public void Visit(RequirementOpenPhoneModel requirementOpenPhoneModel)
    {
        _result.command = new RequirementOpenPhoneCommand(_smartPhone);
    }
    public void Visit(RequirementOpenDUXModel requirementOpenDUXModel)
    {
        _result.command = new RequirementOpenDUXCommand(_smartPhone, _duxWindow);
    }

    public void Visit(AddSympathyModel sympathyModel)
    {
        _result.command = new AddSympthyToCharacterController(sympathyModel, _characterLibrary);
    }
    public void Visit(AccureMoneyModel accureMoneyModel)
    {
        _result.command = new AccureStoregeValueCommand(accureMoneyModel, _wallet, _smartPhone);
    }
    public void Visit(AccureEnergyModel accureEnergyModel)
    {
        _result.command = new AccureStoregeValueCommand(accureEnergyModel, _battery, _smartPhone);
    }

    public void Visit(DecreeseMoneyModel decreeseMoneyModel)
    {
        _result.command = new DecreeseStoregeValueCommand(decreeseMoneyModel, _wallet);
    }
    public void Visit(DecreeseEnergyModel decreeseEnergyModel)
    {
        _result.command = new DecreeseStoregeValueCommand(decreeseEnergyModel, _battery);
    }

    public void Visit(FAQModel faqModel)
    {
        _result.command = new FAQController(faqModel, _faqView, _faqCommander);
    }
    public void Visit(ChangeDialogDataModel changeDialogDataModel)
    {
        _result.node = changeDialogDataModel.NodeGraph.nodes[0];
        (_result.node as XnodeModel).Accept(this);
    }

    public void Visit(MessengerDialogSpeechModel dialogSpeech)
    {
        throw new System.NotImplementedException();
    }
}
