using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private ChatView _chatView;
    [SerializeField] private MessegeFactory _messegeFactory;
    [SerializeField] private MessengerCommander _messengerCommander;
    [SerializeField] private Messenger _messenger;

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

        Container.
            Bind<Messenger>().
            FromInstance(_messenger).
            AsSingle();
    }
}