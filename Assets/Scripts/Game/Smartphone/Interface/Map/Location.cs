using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[Serializable]
public class Location : IDisposable
{
    [SerializeField] private BackgroundView _background;
    [SerializeField] private CollectionPanel _collectionPanel;

    [SerializeField] private Sprite _backgroundSprite;
    [SerializeField] private List<ItemForCollection> _itemsOnLocation;

    [SerializeField] private Node _questOnLocation;

    [field: SerializeField] public LocationType LocationType { get; private set; }
    [field: SerializeField] public string Name { get; private set; }

    public Node QuestOnLocation => _questOnLocation;
    public List<ItemForCollection> Items => _itemsOnLocation;

    public void Initialize(BackgroundView background, CollectionPanel collectionPanel)
    {
        _background = background;
        _collectionPanel = collectionPanel;
    }

    public void Dispose()
    {
        if(_collectionPanel != null)
            _collectionPanel.ItemDeleted -= OnItemDelete;
    }

    public void SetQuest(Node questOnLocation)
    {
        _questOnLocation = questOnLocation;
    }

    public void Add(ItemForCollection item)
    {
        _itemsOnLocation.Add(item);
    }

    public void StartQuest()
    {
        _questOnLocation = null;
    }

    public void Show()
    {
        _collectionPanel.HideItems();
        _background.Replace(_backgroundSprite);

        _background.OnPicturChanged += OnPicturChange;
        _collectionPanel.ItemDeleted += OnItemDelete;
    }

    private void OnItemDelete(ItemForCollection itemData)
    {
        _itemsOnLocation.Remove(itemData);
    }

    private void OnPicturChange()
    {
        _background.OnPicturChanged -= OnPicturChange;
        _collectionPanel.ShowItems(_itemsOnLocation);
    }
}
