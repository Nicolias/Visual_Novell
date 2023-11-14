using UnityEngine;
using Zenject;

public class GameComponentInstaller : MonoInstaller
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Battery _battery;
    [SerializeField] private QuizView _quizView;
    [SerializeField] private AdsServise _adsServise;

    public override void InstallBindings()
    {        
        Container.Bind<Wallet>().FromInstance(_wallet).AsSingle();
        Container.Bind<Battery>().FromInstance(_battery).AsSingle();
        Container.Bind<QuizView>().FromInstance(_quizView).AsSingle();
        Container.Bind<AdsServise>().FromInstance(_adsServise).AsSingle();
        Container.Bind<Quiz>().AsSingle().NonLazy();
    }
}