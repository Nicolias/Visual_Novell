public interface ICommanderVisitor
{
    public void Visit(MonologSpeechModel speech);
    public void Visit(DialogSpeechModel dialogSpeech);
    public void Visit(MessengerDialogSpeechModel dialogSpeech);
    public void Visit(FAQModel faqModel);
    public void Visit(IChoiceModel choice);
    public void Visit(QuizModel quizModel);
    public void Visit(BackgroundModel background);
    public void Visit(ICharacterPortraitModel portrait);
    public void Visit(MiniGameModel miniGameModel);
    public void Visit(AudioModel audio);
    public void Visit(INicknameInputModel nickNameModel);
    public void Visit(NewDialogInSmartphoneModel newMassegemodel);
    public void Visit(SmartphoneGuidModel smartPhoneGuid);
    public void Visit(ICallModel callModel);
    public void Visit(WaitForSecondsModel waitModel);
    public void Visit(SetTimeOnSmartphoneWatchModel timeModel);
    public void Visit(RequirementOpenPhoneModel requirementOpenPhoneModel);
    public void Visit(RequirementOpenDUXModel requirementOpenDUXModel);
    public void Visit(AddSympathyModel sympathyModel);
    public void Visit(AccureMoneyModel accureMoneyModel);
    public void Visit(AccureEnergyModel accureEnergyModel);
    public void Visit(DecreeseMoneyModel decreeseMoneyModel);
    public void Visit(DecreeseEnergyModel decreeseEnergyModel);
    public void Visit(ChangeDialogDataModel changeDialogDataModel);
}