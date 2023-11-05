using Factory;
using Factory.Cells;
using Factory.Messenger;
using Factory.Quiz;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FactoryInstaller : MonoInstaller
{
    [Inject] private StaticData _staticData;

    [SerializeField] private ChatFactory _chatFactory;
    [SerializeField] private MessegeFactory _messegeFactory;
    [SerializeField] private ContactFactory _contactFactory;
    [SerializeField] private CellsFactoryCreater _locationCellFactory;
    [SerializeField] private ChoicePanelFactory _choicePanelFactory;

    public override void InstallBindings()
    {
        Container.Bind<ChatFactory>().FromInstance(_chatFactory).AsSingle();
        Container.Bind<ContactFactory>().FromInstance(_contactFactory).AsSingle();
        Container.Bind<CellsFactoryCreater>().FromInstance(_locationCellFactory).AsSingle();
        Container.Bind<QuestionFactory>().FromMethod(QuestionFactoryBind).AsSingle();

        Container.
            Bind<IChoicePanelFactory>().
            FromInstance(_choicePanelFactory).
            AsSingle();

        Container.
            Bind<MessegeFactory>().
            FromInstance(_messegeFactory).
            AsSingle();
    }

    private QuestionFactory QuestionFactoryBind()
    {
        List<QuizQuestion> questions = new List<QuizQuestion>();

        foreach (var quizQuestion in _staticData.QuizQuestion)
            questions.Add(quizQuestion);

        QuestionFactory questionFactory = new();

        for (int i = 0; i < questions.Count; i++)
            questionFactory.AddQuestionElement(questions[i]);

        return questionFactory;
    }
}