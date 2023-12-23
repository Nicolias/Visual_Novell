
using StateMachine;
using UnityEngine;
using XNode;
using Zenject;


public class GameCommander : Commander, ICommanderVisitor
{
    [Inject] private AudioServise _audioServise;

    [SerializeField] private GameStateMachine _gameSateMachine;

    [SerializeField] private NodeGraph _currentGraph;

    [SerializeField] private MonologSpeechView _monologSpeechView;
    [SerializeField] private DialogSpeechView _dialogSpeechView;

    [SerializeField] private NameInputView _nameInputView;

    [SerializeField] private ChoiceView _choicesView;

    [SerializeField] private CharacterRenderer _portraitView;

    [SerializeField] private BackgroundView _backgroundView;

    [SerializeField] private SmartphoneGuideView _smartphoneGuideView;
    [SerializeField] private GuidView _guidView;

    [SerializeField] private Smartphone _smartPhone;
    [SerializeField] private SmartphoneCallView _callView;

    [SerializeField] private FAQView _faqView;

    [SerializeField] private MiniGameSelector _miniGameSelector;

    [SerializeField] private AccureItemPanel _accureItemPanel;

    private (ICommand command, Node node) _result;

    protected override string SaveKey => "GameCommanderSave";

    private void OnEnable()
    {
        _audioServise.SaveLoader.Load();

        //RuStoreBillingClient.Instance.Init();

        if (_currentGraph == null)
            return;

        if (SaveLoadServise.HasSave(SaveKey))
            Load();
        else
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
        _result.command = DI.Instantiate<DialogSpeechPresenter>(new object[] { dialogSpeech, _dialogSpeechView});
    }

    public void Visit(MonologSpeechModel speech)
    {
        speech.Initialize(StaticData);
        _result.command = DI.Instantiate<MonologSpeechPresenter>(new object[] { speech, _monologSpeechView });
    }

    public void Visit(ICharacterPortraitModel portrait)
    {
        _result.command =DI.Instantiate<CharacterPortraitController>(new object[] { portrait, _portraitView });
    }
    public void Visit(BackgroundModel background)
    {
        _result.command = DI.Instantiate<BackgroundController>(new object[] { background, _backgroundView });
    }

    public void Visit(ChangeLocationModel changeLocationModel)
    {
        _result.command = DI.Instantiate<ChangeLocationController>(new object[] { changeLocationModel});
    }
    public void Visit(IChoiceModel choice)
    {
        _result.command = DI.Instantiate<ChoicesPresentar>(new object[] { choice, _choicesView });
    }

    public void Visit(AudioModel audio)
    {
        _result.command = DI.Instantiate<AudioController>(new object[] { audio });
    }

    public void Visit(INicknameInputModel nickNameModel)
    {
        _result.command = DI.Instantiate<NameInputPresenter>(new object[] { _nameInputView, StaticData });
    }

    public void Visit(NewDialogInSmartphoneModel newMassegemodel)
    {
        _result.command = new SmartphoneNewMessegePresenter(newMassegemodel, _smartPhone.Messenger);
    }

    public void Visit(SetTimeOnSmartphoneWatchModel timeModel)
    {
        _result.command = DI.Instantiate<SetTimeOnSmartphoneCommand>(new object[] { timeModel, _smartPhone });
    }

    public void Visit(ICallModel callModel)
    {
        _result.command = DI.Instantiate<SmartphoneCallPresentar>(new object[] { callModel, _callView });
    }

    public void Visit(SmartphoneGuidModel smartPhoneGuid)
    {
        _result.command = DI.Instantiate<SmartPhoneGuidPresenter>(new object[] { _smartphoneGuideView });
    }

    public void Visit(ShowGuidModel showGuidModel)
    {
        _result.command = new ShowGuidPresenter(showGuidModel, _guidView);
    }

    public void Visit(WaitForSecondsModel waitModel)
    {
        _result.command = DI.Instantiate<WaitForSecondsPresenter>(new object[] {  waitModel });
    }

    public void Visit(RequirementOpenPhoneModel requirementOpenPhoneModel)
    {
        _result.command = new RequirementOpenPhoneCommand(_smartPhone);
    }
    public void Visit(RequirementOpenDUXModel requirementOpenDUXModel)
    {
        _result.command = DI.Instantiate<RequirementOpenDUXCommand>(new object[] { _smartPhone });
    }

    public void Visit(AddSympathyModel sympathyModel)
    {
        _result.command = DI.Instantiate<AddSympthyToCharacterController>(new object[] { sympathyModel });
    }
    public void Visit(AccureMoneyModel accureMoneyModel)
    {
        _result.command = DI.Instantiate<AccureStoregeValueCommand>(new object[] { accureMoneyModel, Wallet});
    }
    public void Visit(AccureEnergyModel accureEnergyModel)
    {
        _result.command = DI.Instantiate<AccureStoregeValueCommand>(new object[] { accureEnergyModel , Battery});
    }

    public void Visit(DecreeseMoneyModel decreeseMoneyModel)
    {
        _result.command = DI.Instantiate<DecreeseStoregeValueCommand>(new object[] { decreeseMoneyModel, Wallet });
    }
    public void Visit(DecreeseEnergyModel decreeseEnergyModel)
    {
        _result.command = DI.Instantiate<DecreeseStoregeValueCommand>(new object[] { decreeseEnergyModel, Battery });
    }

    public void Visit(FAQModel faqModel)
    {
        _result.command = DI.Instantiate<FAQController>(new object[] { faqModel, _faqView });
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

    public void Visit(QuizModel quizModel)
    {
        _result.command = DI.Instantiate<QuizPresenter>(new object[] { quizModel });
    }

    public void Visit(MiniGameModel miniGameModel)
    {
        _result.command = DI.Instantiate<MiniGameController>(new object[] { miniGameModel, _miniGameSelector });
    }

    public void Visit(CollectQuestModel collectQuestModel)
    {
        _result.command = DI.Instantiate<CollectionQuestController>(new object[] { collectQuestModel });
    }

    public void Visit(GetterEnergyItemModel getterEnergyItemModel)
    {
        _result.command = new GetterItemController(_accureItemPanel);
    }

    public void Visit(SwitchSceneModel switchSceneModel)
    {
        _result.command = new SwitchSceneCommand(switchSceneModel.SceneNumber, SaveLoadServise);
    }

    public void Visit(SetQuestOnLocationModel setQuestOnLocation)
    {
        _result.command = DI.Instantiate<SetQuestOnLocationCommand>(new object[] { setQuestOnLocation });
    }

    public void Visit(SpawnItemModel spawnItemModel)
    {
        _result.command = DI.Instantiate<SpawnItemCommand>(new object[] { spawnItemModel });
    }

    public void Visit(ChangeEnabledModel changeEnabledModel)
    {
        _result.command = DI.Instantiate<EnebledController>(new object[] { changeEnabledModel });
    }

    public void Visit(ChapterCaptionModel chapterCaptionModel)
    {
        _result.command = DI.Instantiate<ChapterCaptionCommand>(new object[] { chapterCaptionModel });
    }

    public void Visit(RemoveOrAddLocation removeOrAddLocationModel)
    {
        _result.command = DI.Instantiate<RemoveAndAddLocationCommand>(new object[] { removeOrAddLocationModel });
    }

    public void Visit(GameStateStoryMode gameStateStoryMode)
    {
        _result.command = new ChangeGameStateCommand<StoryState>(_gameSateMachine);
    }

    public void Visit(GameStateFreePlayMode gameStateFreePlayMode)
    {
        _result.command = new ChangeGameStateCommand<PlayState>(_gameSateMachine);
    }

    public void Visit(SenderItemToInventoryModel senderItemToInventory)
    {
        _result.command = DI.Instantiate<SendeItemToInventoryCommand>(new object[] { senderItemToInventory });
    }

    public void Visit(MeetWithPlayerModel meetWithPlayerModel)
    {
        _result.command = DI.Instantiate<MeetWithPlayerCommand>(new object[] { meetWithPlayerModel });
    }
}
