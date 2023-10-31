using Characters;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu(fileName = "New location", menuName = "Location/Location")]
public class Location : ScriptableObject, IDisposable, ISaveLoadObject
{
    [SerializeField] private Sprite _backgroundSprite;
    [SerializeField] private List<ItemForCollection> _itemsOnLocation;

    [SerializeField] private Node _questOnLocation;
    [SerializeField] private bool _isAvilable = true;

    private CharacterOnLocationData _characterOnLocation;

    private CharactersPortraitView _charactersViewServise;
    private BackgroundView _background;
    private CollectionPanel _collectionPanel;
    private List<ItemForCollectionView> _itemsView = new List<ItemForCollectionView>();

    [field: SerializeField] public string Name { get; private set; }

    public bool IsAvilable => _isAvilable;

    public IEnumerable<ItemForCollection> Items => _itemsOnLocation;
    public IEnumerable<ItemForCollectionView> ItemsView => _itemsView;

    public event Action<Node> QuestStarted;

    public void Initialize(BackgroundView background, CollectionPanel collectionPanel, CharactersPortraitView charactersPortraitView)
    {
        _background = background;
        _collectionPanel = collectionPanel;
        _charactersViewServise = charactersPortraitView;

        _itemsView = collectionPanel.CreateItemsView(Items);
    }

    public void Disable()
    {
        _isAvilable = false;
    }

    public void Show()
    {
        if (_questOnLocation != null)
            QuestStarted?.Invoke(_questOnLocation);

        _background.Replace(_backgroundSprite);

        _collectionPanel.ShowItems(_itemsView);
        _collectionPanel.ItemDeleted += OnItemDelete;
    }

    public void Dispose()
    {
        if (_collectionPanel != null)
            _collectionPanel.ItemDeleted -= OnItemDelete;
    }

    public void Add(ItemForCollection item)
    {
        _itemsOnLocation.Add(item);
        _itemsView.AddRange(_collectionPanel.CreateItemsView(new List<ItemForCollection>() { item }));
    }

    public void Set(Node questOnLocation)
    {
        _questOnLocation = questOnLocation;
    }

    public void Invite(Character character)
    {
        Show();
        _charactersViewServise.Show(Get(character));
    }

    protected virtual CharacterOnLocationData Get(Character character)
    {
        throw new InvalidProgramException("Нужно создать отдельный тип для локации");
    }

    private void OnItemDelete(ItemForCollection itemData)
    {
        _itemsOnLocation.Remove(itemData);
        _itemsView.RemoveAll(x => x == null);
    }

    public void Save()
    {
        throw new NotImplementedException();
    }

    public void Load()
    {
        throw new NotImplementedException();
    }
}

public class CharacterOnLocationData : ICharacterPortraitModel
{
    public CharacterOnLocationData(CharacterType characterType, string name, Sprite sprite, 
        CharacterPortraitPosition positionType, Vector2 positionOffset, Vector3 scaleOffset)
    {
        CharacterType = characterType;
        Name = name;
        Sprite = sprite;
        PositionType = positionType;
        PositionOffset = positionOffset;
        ScaleOffset = scaleOffset;
    }

    public CharacterType CharacterType { get; private set; }

    public string Name { get; private set; }

    public Sprite Sprite { get; private set; }

    public CharacterPortraitPosition PositionType { get; private set; }

    public Vector2 PositionOffset { get; private set; }

    public Vector3 ScaleOffset { get; private set; }
}