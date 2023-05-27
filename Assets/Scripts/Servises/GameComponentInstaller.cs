using UnityEngine;
using Zenject;

public class GameComponentInstaller : MonoInstaller
{
    [SerializeField] private CharactersLibrary _charactersLibrary;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Battery _battery;
    [SerializeField] private QuizView _quizView;

    public override void InstallBindings()
    {
        Container.Bind<CharactersLibrary>().FromInstance(_charactersLibrary).AsSingle();
        Container.Bind<Wallet>().FromInstance(_wallet).AsSingle();
        Container.Bind<Battery>().FromInstance(_battery).AsSingle();
        Container.Bind<QuizView>().FromInstance(_quizView).AsSingle();
        Container.Bind<Quiz>().AsSingle().NonLazy();
    }
}