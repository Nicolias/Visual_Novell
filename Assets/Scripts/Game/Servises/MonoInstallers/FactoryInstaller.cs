using Factory.CellLocation;
using Factory.Messenger;
using Factory.Quiz;
using UnityEngine;
using Zenject;

public class FactoryInstaller : MonoInstaller
{
    [Inject] private StaticData _staticData;

    [SerializeField] private ChatFactory _chatFactory;
    [SerializeField] private MessegeFactory _messegeFactory;
    [SerializeField] private ContactFactory _contactFactory;
    [SerializeField] private LocationCellFactory _locationCellFactory;

    public override void InstallBindings()
    {
        Container.Bind<ChatFactory>().FromInstance(_chatFactory).AsSingle();
        Container.Bind<ContactFactory>().FromInstance(_contactFactory).AsSingle();
        Container.Bind<ILocationCellsFactory>().FromInstance(_locationCellFactory).AsSingle();
        Container.Bind<QuestionFactory>().FromMethod(QuestionFactoryBind).AsSingle();
        Container.
            Bind<MessegeFactory>().
            FromInstance(_messegeFactory).
            AsSingle();
    }

    private QuestionFactory QuestionFactoryBind()
    {
        QuestionFactory questionFactory = new();

        for (int i = 0; i < _staticData.QuizQuestion.Count; i++)
            questionFactory.AddQuestionElement(_staticData.QuizQuestion[i]);

        return questionFactory;
    }
}