using UnityEngine;
using Zenject;

public class ServiseInstaller : MonoInstaller
{
    [SerializeField] private StaticData _staticData;
    [SerializeField] private CoroutineServise _coroutineServise;
    [SerializeField] private AudioServise _audioServise;

    public override void InstallBindings()
    {
        Container.Bind<StaticData>().FromComponentInNewPrefab(_staticData).AsSingle();
        Container.Bind<CoroutineServise>().FromInstance(_coroutineServise).AsSingle();
        Container.Bind<AudioServise>().FromInstance(_audioServise).AsSingle();
    }
}
