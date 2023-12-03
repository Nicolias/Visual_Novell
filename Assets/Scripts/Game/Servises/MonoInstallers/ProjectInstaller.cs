using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private SaveLoadServise _saveLoadServise;
    [SerializeField] private TimesOfDayServise _timesOfDayServise;
    [SerializeField] private AudioServise _audioServise;

    public override void InstallBindings()
    {
        Container.Bind<SaveLoadServise>().FromComponentsInNewPrefab(_saveLoadServise).AsSingle();
        Container.Bind<TimesOfDayServise>().FromComponentInNewPrefab(_timesOfDayServise).AsSingle().NonLazy();
        Container.Bind<AudioServise>().FromComponentsInNewPrefab(_audioServise).AsSingle().NonLazy();
    }
}