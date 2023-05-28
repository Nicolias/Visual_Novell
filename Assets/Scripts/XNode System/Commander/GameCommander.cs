﻿using UnityEngine;
using XNode;
using Zenject;

public class GameCommander : Commander, ICommanderVisitor
{
    

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
        _result.command = DI.Instantiate<SpeechPresentar>(new object[] { dialogSpeech, _dialogSpeechView});
    }
    public void Visit(MonologSpeechModel speech)
    {
        speech.Initialize(StaticData);
        _result.command = DI.Instantiate<SpeechPresentar>(new object[] { speech, _monologSpeechView });
    }

    public void Visit(ICharacterPortraitModel portrait)
    {
        _result.command =DI.Instantiate<CharacterPortraitController>(new object[] { portrait, _portraitView });
    }
    public void Visit(BackgroundModel background)
    {
        _result.command = DI.Instantiate<BackgroundController>(new object[] { background, _backgroundView });
    }
    public void Visit(IChoiceModel choice)
    {
        _result.command = DI.Instantiate<ChoicesPresentar>(new object[] { choice, _choicesView });
    }
    public void Visit(AudioModel audio)
    {
        _result.command = DI.Instantiate<AudioController>(new object[] { audio, _audioServise });
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
        _result.command = DI.Instantiate<RequirementOpenDUXCommand>(new object[] { _smartPhone, _duxWindow });
    }

    public void Visit(AddSympathyModel sympathyModel)
    {
        _result.command = DI.Instantiate<AddSympthyToCharacterController>(new object[] { sympathyModel, _characterLibrary });
    }
    public void Visit(AccureMoneyModel accureMoneyModel)
    {
        _result.command = DI.Instantiate<AccureStoregeValueCommand>(new object[] { accureMoneyModel });
    }
    public void Visit(AccureEnergyModel accureEnergyModel)
    {
        _result.command = DI.Instantiate<AccureStoregeValueCommand>(new object[] { accureEnergyModel });
    }

    public void Visit(DecreeseMoneyModel decreeseMoneyModel)
    {
        _result.command = DI.Instantiate<DecreeseStoregeValueCommand>(new object[] { decreeseMoneyModel });
    }
    public void Visit(DecreeseEnergyModel decreeseEnergyModel)
    {
        _result.command = DI.Instantiate<DecreeseStoregeValueCommand>(new object[] { decreeseEnergyModel });
    }

    public void Visit(FAQModel faqModel)
    {
        _result.command = DI.Instantiate<FAQController>(new object[] { faqModel, _faqView, _faqCommander });
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
