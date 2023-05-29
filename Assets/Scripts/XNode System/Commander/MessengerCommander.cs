using XNode;
using Zenject;

public class MessengerCommander : Commander, ICommanderVisitor
{
    [Inject] private ChatView _chatView;

    private (ICommand command, Node node) _result;

    protected override (ICommand, Node) Packing(Node node)
    {
        _result.node = node;
        (node as XnodeModel).Accept(this);

        return _result;
    }

    public void Visit(MonologSpeechModel speech)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(DialogSpeechModel dialogSpeech)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(MessengerDialogSpeechModel dialogSpeech)
    {
        dialogSpeech.Initialize(StaticData);
        _result.command = new MessengerDialogPresenter(dialogSpeech, _chatView, _result.node, StaticData);
    }

    public void Visit(FAQModel faqModel)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(IChoiceModel choice)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(BackgroundModel background)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(ICharacterPortraitModel portrait)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(AudioModel audio)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(INicknameInputModel nickNameModel)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(NewDialogInSmartphoneModel newMassegemodel)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(SmartphoneGuidModel smartPhoneGuid)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(ICallModel callModel)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(WaitForSecondsModel waitModel)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(SetTimeOnSmartphoneWatchModel timeModel)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(RequirementOpenPhoneModel requirementOpenPhoneModel)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(RequirementOpenDUXModel requirementOpenDUXModel)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(AddSympathyModel sympathyModel)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(AccureMoneyModel accureMoneyModel)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(AccureEnergyModel accureEnergyModel)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(DecreeseMoneyModel decreeseMoneyModel)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(DecreeseEnergyModel decreeseEnergyModel)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(ChangeDialogDataModel changeDialogDataModel)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(QuizModel quizModel)
    {
        throw new System.NotImplementedException();
    }

    public void Visit(MiniGameModel miniGameModel)
    {
        throw new System.NotImplementedException();
    }
}
