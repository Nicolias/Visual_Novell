using UnityEngine;
using Zenject;

public class ServiseInstaller : MonoInstaller
{
    [SerializeField] private StaticData _staticData;
    [SerializeField] private CharactersLibrary _charactersLibrary;
    [SerializeField] private CoroutineServise _coroutineServise;
    [SerializeField] private DUXWindow _dUXWindow;

    public override void InstallBindings()
    {
        Container.Bind<StaticData>().FromComponentInNewPrefab(_staticData).AsSingle();
        Container.Bind<CharactersLibrary>().FromInstance(_charactersLibrary).AsSingle().NonLazy();
        Container.Bind<DUXWindow>().FromComponentsInNewPrefab(_dUXWindow).AsSingle().NonLazy();
        Container.Bind<CoroutineServise>().FromComponentsInNewPrefab(_coroutineServise).AsSingle();
    }
}
