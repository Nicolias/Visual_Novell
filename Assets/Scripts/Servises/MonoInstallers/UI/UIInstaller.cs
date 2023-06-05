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
    [SerializeField] private Smartphone _smartphone;
    [SerializeField] private BackgroundView _background;
    [SerializeField] private CollectionPanel _collectionPanel;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private CollectionQuestView _collectionQuestView;

    public override void InstallBindings()
    {
        Container.Bind<CollectionQuestView>().FromInstance(_collectionQuestView).AsSingle();

        Container.
            Bind<Smartphone>().
            FromInstance(_smartphone).
            AsSingle();

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
        Container.Bind<CollectionPanel>().FromInstance(_collectionPanel).AsSingle();
        Container.Bind<Inventory>().FromInstance(_inventory).AsSingle();
    }
}