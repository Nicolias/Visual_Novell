using System;
using System.Collections.Generic;

public class CollectionQuest : Quest, IItemCollector, IDisposable
{
    private readonly CollectionPanel _collectionPanel;

    private readonly Inventory _inventory;

    private List<ItemForCollection> _itemsForCollection = new();
    private ItemForCollection _currentItemData;

    public IEnumerable<ItemForCollection> ItemForCollections => _itemsForCollection;

    public event Action QuestCompleted;
    public event Action<ItemForCollection> ItemCollected;

    public CollectionQuest(List<ItemForCollection> itemsForCollection, CollectionPanel collectionPanel, Inventory inventory)
    {
        _collectionPanel = collectionPanel;
        _inventory = inventory;

        _itemsForCollection.AddRange(itemsForCollection);

        if(_itemsForCollection.Count > 0)
            _currentItemData = _itemsForCollection[0];

        _collectionPanel.ItemSelected += OnItemSelected;
    }

    public void Dispose()
    {
        _collectionPanel.ItemSelected -= OnItemSelected;
    }

    private void OnItemSelected(ItemForCollectionView selectdItemView, ItemForCollection selectedItemData)
    {
        if (_currentItemData == null)
            throw new InvalidOperationException("Нет текущего предмета в списки заданий по сбору.");

        if (_currentItemData != selectedItemData)
            return;

        if (_itemsForCollection.Contains(selectedItemData))
        {
            _itemsForCollection.Remove(selectedItemData);

            if (_itemsForCollection.Count != 0)
                _currentItemData = _itemsForCollection[0];
            else
                QuestCompleted?.Invoke();

            selectedItemData.Acсept(this);

            _collectionPanel.Delete(selectdItemView, selectedItemData);

            ItemCollected?.Invoke(selectedItemData);
        }
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