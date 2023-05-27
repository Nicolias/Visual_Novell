using Factory.Messenger;
using Factory.Quiz;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FactoryInstaller : MonoInstaller
{
    [Inject] private StaticData _staticData;

    [SerializeField] private ChatFactory _chatFactory;
    [SerializeField] private ContactFactory _contactFactory;

    public override void InstallBindings()
    {
        Container.Bind<ChatFactory>().FromInstance(_chatFactory).AsSingle();
        Container.Bind<ContactFactory>().FromInstance(_contactFactory).AsSingle();
        Container.Bind<QuestionFactory>().FromMethod(QuestionFactoryBind);
    }

    private QuestionFactory QuestionFactoryBind()
    {
        QuestionFactory questionFactory = new();

        for (int i = 0; i < _staticData.QuizQuestion.Count; i++)
            questionFactory.AddQuestionElement(_staticData.QuizQuestion[i]);

        return questionFactory;
    }
}