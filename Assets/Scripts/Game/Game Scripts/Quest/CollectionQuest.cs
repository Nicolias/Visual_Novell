using System;
using System.Collections.Generic;

public class CollectionQuest : Quest, IItemCollector
{
    private readonly CollectionPanel _collectionPanel;

    private readonly IInventory _inventory;

    private List<ItemForCollection> _itemsForCollection = new();
    private ItemForCollection _currentItemData;

    public IEnumerable<ItemForCollection> ItemsForCollection => _itemsForCollection;

    public event Action QuestCompleted;
    public event Action<ItemForCollection> ItemCollected;

    public CollectionQuest(List<ItemForCollection> itemsForCollectionData, CollectionPanel collectionPanel, IInventory inventory)
    {
        _collectionPanel = collectionPanel;
        _inventory = inventory;

        _itemsForCollection.AddRange(itemsForCollectionData);

        if(itemsForCollectionData.Count > 0)
            _currentItemData = itemsForCollectionData[0];

        _collectionPanel.ItemSelected += OnItemSelected;
    }

    private void OnItemSelected(ItemForCollectionView selectdItemView, ItemForCollection selectedItemData)
    {
        if (_currentItemData == null)
            throw new InvalidOperationException("Нет предметов для сбора.");

        if (selectdItemView == null)
            throw new InvalidOperationException("Предмет уже удален");

        if (_currentItemData != selectdItemView.Data)
            return;

        if (_itemsForCollection.Contains(selectedItemData))
        {
            ItemCollected?.Invoke(selectdItemView.Data);

            _itemsForCollection.Remove(selectedItemData);
            selectdItemView.Data.Acсept(this);
            _collectionPanel.Delete(selectdItemView, selectdItemView.Data);

            if (_itemsForCollection.Count == 0)
            {
                _collectionPanel.ItemSelected -= OnItemSelected;
                QuestCompleted?.Invoke();
            }
            else
            {
                _currentItemData = _itemsForCollection[0];
            }
        }
    }

    public void Visit(BackpackItem backpackItem)
    {
        _inventory.InventoryComponent.enabled = true;
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