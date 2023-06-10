using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private SaveLoadServise _saveLoadServise;
    [SerializeField] private TimesOfDayServise _timesOfDayServise;

    public override void InstallBindings()
    {
        Container.Bind<SaveLoadServise>().FromComponentsInNewPrefab(_saveLoadServise).AsSingle();
        Container.Bind<TimesOfDayServise>().FromComponentInNewPrefab(_timesOfDayServise).AsSingle();
    }
}