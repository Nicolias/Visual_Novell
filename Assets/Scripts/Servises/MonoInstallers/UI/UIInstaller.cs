using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private ChatView _chatView;
    [SerializeField] private MessegeFactory _messegeFactory;
    [SerializeField] private MessengerCommander _messengerCommander;

    public override void InstallBindings()
    {
        Container.
            Bind<ChatView>().
            FromInstance(_chatView).
            AsSingle();

        Container.
            Bind<MessegeFactory>().
            FromInstance(_messegeFactory).
            AsSingle();

        Container.
            Bind<MessengerCommander>().
            FromInstance(_messengerCommander).
            AsSingle();
    }
}