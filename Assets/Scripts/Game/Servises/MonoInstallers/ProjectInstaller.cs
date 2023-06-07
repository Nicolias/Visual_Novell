using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private TimesOfDayServise _timesOfDayServise;

    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().FromComponentsInNewPrefab(_sceneLoader).AsSingle();
        Container.Bind<TimesOfDayServise>().FromComponentInNewPrefab(_timesOfDayServise).AsSingle();
    }
}