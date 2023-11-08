using Characters;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu(fileName = "New location", menuName = "Location/Location")]
public class Location : ScriptableObject, IDisposable, ISaveLoadObject, IDataForCell
{
    [SerializeField] private Sprite _backgroundSprite;
    [SerializeField] private List<ItemForCollection> _itemsOnLocation;

    [SerializeField] private Node _questOnLocation;
    [SerializeField] private bool _isAvilable = true;

    [Header("Настройки положения персонажа на локации")]
    [SerializeField] private Vector2 _characterPosition;
    [SerializeField] private Vector3 _characterScale;

    [SerializeField] private CharacterPoseType _characterPoseType;

    [SerializeField] private List<PastimeOnLocationType> _actionsOnLocation;

    private CharacterOnLocationData _characterOnLocation;

    private CharacterRenderer _charactersRenderer;
    private BackgroundView _background;
    private CollectionPanel _collectionPanel;
    private List<ItemForCollectionView> _itemsView = new List<ItemForCollectionView>();

    [field: SerializeField] public string Name { get; private set; }

    public bool IsAvilable => _isAvilable;

    public IEnumerable<ItemForCollection> Items => _itemsOnLocation;
    public IEnumerable<ItemForCollectionView> ItemsView => _itemsView;
    public IEnumerable<PastimeOnLocationType> ActionsList => _actionsOnLocation;

    public event Action<Location, Node> QuestStarted;

    public void Initialize(BackgroundView background, CollectionPanel collectionPanel, CharacterRenderer charactersPortraitView)
    {
        _background = background;
        _collectionPanel = collectionPanel;
        _charactersRenderer = charactersPortraitView;

        _itemsView = collectionPanel.CreateItemsView(Items);
    }

    public void Disable()
    {
        _isAvilable = false;
    }

    public void Show()
    {
        if (_questOnLocation != null)
            QuestStarted?.Invoke(this, _questOnLocation);

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

    public void RemoveQuest()
    {
        _questOnLocation = null;
    }

    public void Invite(Character character)
    {
        _charactersRenderer.DeleteAllCharacters();
        Show();
        _charactersRenderer.Show(Get(character));
    }

    private CharacterOnLocationData Get(Character character)
    {
        if (character.Images.TryGet(_characterPoseType, out Sprite characterSprite))
            return new CharacterOnLocationData(character.Type, character.Name, characterSprite, this,
                    CharacterPortraitPosition.FreePosition, _characterPosition, _characterScale);
        else
            throw new InvalidOperationException("Эта локация не предназначена для приглашения.");
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
