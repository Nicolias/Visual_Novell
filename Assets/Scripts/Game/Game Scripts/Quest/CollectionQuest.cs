using System;
using System.Collections.Generic;

public class CollectionQuest : Quest, IItemCollector
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

        item.Acсept(this);

        OnItemCollected?.Invoke(item);

        return true;
    }

    public void Visit(BackpackItem backpackItem)
    {
        _inventory.enabled = true;
    }

    public void Visit(KeyItem keyItem)
    {
        _inventory.AddItemToInventory(keyItem);
    }

    public void Visit(StarItem starItem)
    {
        _inventory.AddItemToInventory(starItem);
    }
}
