using QuizSystem;
using StateMachine;
using UnityEngine;
using Zenject;

public class GameComponentInstaller : MonoInstaller
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Battery _battery;
    [SerializeField] private QuizView _quizView;
    [SerializeField] private MiniGameSelector _miniGameSelector;
    [SerializeField] private GameStateMachine _gameStateMachine;
    [SerializeField] private LocationsManager _locationsManager;
    
    public override void InstallBindings()
    {        
        Container.Bind<Wallet>().FromInstance(_wallet).AsSingle();
        Container.Bind<Battery>().FromInstance(_battery).AsSingle();
        Container.Bind<MiniGameSelector>().FromInstance(_miniGameSelector).AsSingle();
        Container.Bind<QuizView>().FromInstance(_quizView).AsSingle();
        Container.Bind<GameStateMachine>().FromInstance(_gameStateMachine).AsSingle();
        Container.Bind<Quiz>().AsSingle().NonLazy();
        Container.Bind<LocationsManager>().FromInstance(_locationsManager).AsSingle();
    }
}