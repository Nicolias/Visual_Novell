using System;
using System.Collections;
using System.Collections.Generic;

public class CollectionQuest : Quest
{
    public event Action OnQuestCompleted;

    public event Action<ItemForCollection> OnItemCollected;

    private readonly CollectionPanel _collectionPanel;
    private readonly Inventory _inventory;

    private List<ItemForCollection> _itemForCollections = new();
    private ItemForCollection _currentItem;

    public IEnumerable<ItemForCollection> ItemForCollections => _itemForCollections;

    public CollectionQuest(List<ItemForCollection> itemForCollections, CollectionPanel collectionPanel, Inventory inventory)
    {
        _collectionPanel = collectionPanel;
        _inventory = inventory;

        _itemForCollections.AddRange(itemForCollections);
        if(_itemForCollections.Count > 0)
            _currentItem = _itemForCollections[0];

        _collectionPanel.IsItemDelete += OnItemCollect;
    }

    private bool OnItemCollect(ItemForCollection item)
    {
        if (_currentItem != item)
            return false;

        if (_itemForCollections.Contains(item))
            _itemForCollections.Remove(item);

        _currentItem = _itemForCollections.Count != 0 ? _itemForCollections[0] : null;

        if (_currentItem == null)
            OnQuestCompleted?.Invoke();

        _inventory.AddItemToInventory(item);

        OnItemCollected?.Invoke(item);

        return true;
    }
}
