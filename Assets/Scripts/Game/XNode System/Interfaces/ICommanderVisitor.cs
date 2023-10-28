﻿public interface ICommanderVisitor
{
    void Visit(MonologSpeechModel speech);
    void Visit(DialogSpeechModel dialogSpeech);
    void Visit(SetQuestOnLocationModel setQuestOnLocation);
    void Visit(MessengerDialogSpeechModel dialogSpeech);
    void Visit(FAQModel faqModel);
    void Visit(IChoiceModel choice);
    void Visit(QuizModel quizModel);
    void Visit(BackgroundModel background);
    void Visit(ChangeLocationModel changeLocationModel);
    void Visit(ICharacterPortraitModel portrait);
    void Visit(RemoveOrAddLocation deleteLocationFromMap);
    void Visit(SpawnItemModel spawnItemModel);
    void Visit(ChapterCaptionModel chapterCaptionModel);
    void Visit(SwitchSceneModel switchSceneModel);
    void Visit(GetterEnergyItemModel getterEnergyItemModel);
    void Visit(CollectQuestModel collectQuestModel);
    void Visit(MiniGameModel miniGameModel);
    void Visit(AudioModel audio);
    void Visit(INicknameInputModel nickNameModel);
    void Visit(NewDialogInSmartphoneModel newMassegemodel);
    void Visit(ChangeEnabledModel changeEnabledModel);
    void Visit(SmartphoneGuidModel smartPhoneGuid);
    void Visit(ICallModel callModel);
    void Visit(WaitForSecondsModel waitModel);
    void Visit(SetTimeOnSmartphoneWatchModel timeModel);
    void Visit(RequirementOpenPhoneModel requirementOpenPhoneModel);
    void Visit(RequirementOpenDUXModel requirementOpenDUXModel);
    void Visit(AddSympathyModel sympathyModel);
    void Visit(AccureMoneyModel accureMoneyModel);
    void Visit(AccureEnergyModel accureEnergyModel);
    void Visit(DecreeseMoneyModel decreeseMoneyModel);
    void Visit(DecreeseEnergyModel decreeseEnergyModel);
    void Visit(ChangeDialogDataModel changeDialogDataModel);
}