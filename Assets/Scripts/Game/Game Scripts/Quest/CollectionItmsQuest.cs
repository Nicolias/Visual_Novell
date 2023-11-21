using System;
using System.Collections.Generic;
using System.Linq;

public class CollectionItmsQuest : CollectionQuest<ItemForCollection>, ItemCollector
{
    private readonly IInventory _inventory;

    private ItemForCollection _currentItemData;

    public CollectionItmsQuest(IEnumerable<ItemForCollection> itemsForCollection, CollectionPanel collectionPanel, IInventory inventory) : base(collectionPanel, itemsForCollection)
    {
        _inventory = inventory;

        if (itemsForCollection.Count() > 0)
            _currentItemData = itemsForCollection.First();
    }

    public override event Action<ItemForCollection> ItemCollected;

    protected override void OnItemSelected(ItemForCollectionView selectdItemView, ItemForCollection selectedItemData)
    {
        if (_currentItemData == null)
            throw new InvalidOperationException("Нет предметов для сбора.");

        if (selectdItemView == null)
            throw new InvalidOperationException("Предмет уже удален");

        if (_currentItemData != selectdItemView.Data)
            return;

        if (TryDeleteItem(selectdItemView, selectedItemData))
        {
            ItemCollected?.Invoke(selectdItemView.Data);
            selectdItemView.Data.Acсept(this);

            if(ItemsForCollection.Count() > 0)
                _currentItemData = ItemsForCollection.First();
        }
    }

    void ItemCollector.Visit(BackpackItem backpackItem)
    {
        _inventory.InventoryComponent.enabled = true;
    }

    void ItemCollector.Visit(KeyItem keyItem)
    {
        _inventory.AddItemToInventory(keyItem);
    }

    void ItemCollector.Visit(StarItem starItem)
    {
        _inventory.AddItemToInventory(starItem);
    }
}
