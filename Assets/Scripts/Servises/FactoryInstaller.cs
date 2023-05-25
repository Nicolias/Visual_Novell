using Factory.Messenger;
using UnityEngine;
using Zenject;

public class FactoryInstaller : MonoInstaller
{
    [SerializeField] private ChatFactory _chatFactory;
    [SerializeField] private ContactFactory _contactFactory;

    public override void InstallBindings()
    {
        Container.Bind<ChatFactory>().FromInstance(_chatFactory).AsSingle();
        Container.Bind<ContactFactory>().FromInstance(_contactFactory).AsSingle();
    }
}