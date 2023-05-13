using UnityEngine;
using Zenject;

public class ServiseInstaller : MonoInstaller
{
    [SerializeField] private StaticData _staticData;

    public override void InstallBindings()
    {
       Container.Bind<StaticData>().FromComponentOn(_staticData.gameObject).AsSingle();
    }
}