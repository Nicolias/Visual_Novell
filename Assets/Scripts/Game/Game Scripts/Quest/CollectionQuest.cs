using System;
using System.Collections.Generic;

public class CollectionQuest : Quest, IItemCollector, IDisposable
{
    private readonly CollectionPanel _collectionPanel;

    private readonly IInventory _inventory;

    private List<ItemForCollectionView> _itemsForCollection = new();
    private ItemForCollection _currentItemData;

    public IEnumerable<ItemForCollectionView> ItemsForCollection => _itemsForCollection;
    public bool IsQuestComplete { get; private set; }

    public event Action QuestCompleted;
    public event Action<ItemForCollection> ItemCollected;

    public CollectionQuest(List<ItemForCollection> itemsForCollectionData, CollectionPanel collectionPanel, IInventory inventory)
    {
        _collectionPanel = collectionPanel;
        _inventory = inventory;

        _itemsForCollection = _collectionPanel.CreateItemsView(itemsForCollectionData);
        _collectionPanel.ShowItems(_itemsForCollection);

        if(_itemsForCollection.Count > 0)
            _currentItemData = _itemsForCollection[0].Data;

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

        if (selectdItemView == null)
            throw new InvalidOperationException("Предмет уже удален");

        if (_currentItemData != selectdItemView.Data)
            return;

        if (_itemsForCollection.Contains(selectdItemView))
        {
            _itemsForCollection.Remove(selectdItemView);

            if (_itemsForCollection.Count != 0)
            {
                _currentItemData = _itemsForCollection[0].Data;
            }
            else
            {
                QuestCompleted?.Invoke();
                IsQuestComplete = true;
            }

            selectdItemView.Data.Acсept(this);
            _collectionPanel.Delete(selectdItemView, selectdItemView.Data);

            ItemCollected?.Invoke(selectdItemView.Data);
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