using UnityEngine;
using Zenject;

public class ServiseInstaller : MonoInstaller
{
    [SerializeField] private StaticData _staticData;
    [SerializeField] private CharactersLibrary _charactersLibrary;
    [SerializeField] private CoroutineServise _coroutineServise;
    [SerializeField] private DUXWindow _dUXWindow;
    [SerializeField] private AuntificationServise _auntificationServise;

    public override void InstallBindings()
    {
        Container.Bind<StaticData>().FromComponentInNewPrefab(_staticData).AsSingle().NonLazy();
        Container.Bind<CharactersLibrary>().FromComponentInNewPrefab(_charactersLibrary).AsSingle().NonLazy();
        Container.Bind<DUXWindow>().FromComponentsInNewPrefab(_dUXWindow).AsSingle().NonLazy();
        Container.Bind<CoroutineServise>().FromComponentsInNewPrefab(_coroutineServise).AsSingle();
        Container.Bind<AuntificationServise>().FromComponentInNewPrefab(_auntificationServise).AsSingle();
    }
}
