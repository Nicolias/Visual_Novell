using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private ChatView _chatView;
    [SerializeField] private MessegeFactory _messegeFactory;
    [SerializeField] private MessengerCommander _messengerCommander;
    [SerializeField] private Messenger _messenger;
    [SerializeField] private ChoicePanel _choisePanel;
    [SerializeField] private Map _map;
    [SerializeField] private BackgroundView _background;

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

        Container.Bind<ChoicePanel>().FromInstance(_choisePanel).AsSingle();
        Container.Bind<Map>().FromInstance(_map).AsSingle();
        Container.Bind<BackgroundView>().FromInstance(_background).AsSingle();
    }
}