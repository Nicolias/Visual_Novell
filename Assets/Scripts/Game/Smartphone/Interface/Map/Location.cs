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

    private List<ItemForCollectionView> _itemsOnLocationView = new List<ItemForCollectionView>(); 

    public Node QuestOnLocation => _questOnLocation;
    public List<ItemForCollection> Items => _itemsOnLocation;

    public void Initialize(BackgroundView background, CollectionPanel collectionPanel)
    {
        _background = background;
        _collectionPanel = collectionPanel;

        _itemsOnLocationView = collectionPanel.CreateItemsView(_itemsOnLocation);
    }

    public void Show()
    {
        _collectionPanel.HideItems();
        _background.Show(_backgroundSprite);

        _collectionPanel.ShowItems(_itemsOnLocationView);
        _collectionPanel.ItemDeleted += OnItemDelete;
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
        _itemsOnLocationView.AddRange(_collectionPanel.CreateItemsView(new() { item }));
    }

    public void StartQuest()
    {
        _questOnLocation = null;
    }

    private void OnItemDelete(ItemForCollection itemData)
    {
        _itemsOnLocation.Remove(itemData);
        _itemsOnLocationView.RemoveAll(x => x == null);
    }
}
