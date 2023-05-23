using UnityEngine;
using Zenject;

public class GameComponentInstaller : MonoInstaller
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Battery _battery;

    public override void InstallBindings()
    {
        Container.Bind<Wallet>().FromInstance(_wallet).AsSingle();
        Container.Bind<Battery>().FromInstance(_battery).AsSingle();
    }
}