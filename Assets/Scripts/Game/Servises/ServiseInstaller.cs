using UnityEngine;
using Zenject;

public class ServiseInstaller : MonoInstaller
{
    [SerializeField] private StaticData _staticData;
    [SerializeField] private CoroutineServise _coroutineServise;
    [SerializeField] private AudioServise _audioServise;
    [SerializeField] private DUXWindow _dUXWindow;

    public override void InstallBindings()
    {
        Container.Bind<DUXWindow>().FromComponentsInNewPrefab(_dUXWindow).AsSingle();
        Container.Bind<StaticData>().FromComponentInNewPrefab(_staticData).AsSingle();
        Container.Bind<CoroutineServise>().FromComponentsInNewPrefab(_coroutineServise).AsSingle();
        Container.Bind<AudioServise>().FromComponentsInNewPrefab(_audioServise).AsSingle().NonLazy();
    }
}
