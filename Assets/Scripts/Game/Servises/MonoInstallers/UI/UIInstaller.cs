using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private ChoicePanel _choicePanel;
    [SerializeField] private ChatWindow _chatView;
    [SerializeField] private MessengerCommander _messengerCommander;
    [SerializeField] private Messenger _messenger;
    [SerializeField] private MessengerWindow _messengerWindow;
    [SerializeField] private Map _map;
    [SerializeField] private Smartphone _smartphone;
    [SerializeField] private BackgroundView _background;
    [SerializeField] private CollectionPanel _collectionPanel;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private CollectionQuestView _collectionQuestView;
    [SerializeField] private FAQCommander _fAQCommander;
    [SerializeField] private ChapterCaption _chapterCaption;

    [SerializeField] private CharacterRenderer _charactersPortraitView;

    public override void InstallBindings()
    {
        Container.Bind<CollectionQuestView>().FromInstance(_collectionQuestView).AsSingle();
        Container.Bind<FAQCommander>().FromInstance(_fAQCommander).AsSingle();

        Container.
            Bind<Smartphone>().
            FromInstance(_smartphone).
            AsSingle();

        Container.
            Bind<ChoicePanel>().
            FromInstance(_choicePanel).
            AsSingle();

        Container.
            Bind<IChatWindow>().
            FromInstance(_chatView).
            AsSingle();

        Container.
            Bind<MessengerCommander>().
            FromInstance(_messengerCommander).
            AsSingle();

        Container.
            Bind<IMessengerWindow>().
            FromInstance(_messengerWindow).
            AsSingle();

        Container.
            Bind<Messenger>().
            FromInstance(_messenger).
            AsSingle();

        Container.Bind<Map>().FromInstance(_map).AsSingle();
        Container.Bind<BackgroundView>().FromInstance(_background).AsSingle();
        Container.Bind<CollectionPanel>().FromInstance(_collectionPanel).AsSingle();
        Container.Bind<IInventory>().FromInstance(_inventory).AsSingle();
        Container.Bind<ChapterCaption>().FromInstance(_chapterCaption).AsSingle();

        Container.Bind<CharacterRenderer>().FromInstance(_charactersPortraitView).AsSingle();
    }
}