using System;
using System.Collections.Generic;
using System.Linq;

public abstract class CollectionQuest<T> where T : ItemForCollection
{
    protected readonly CollectionPanel CollectionPanel;

    private readonly List<ItemForCollection> _itemsForCollection;

    public CollectionQuest(CollectionPanel collectionPanel, IEnumerable<ItemForCollection> itemsForCollection)
    {
        _itemsForCollection = itemsForCollection.ToList();

        CollectionPanel = collectionPanel;
        CollectionPanel.ItemSelected += OnItemSelected;
    }

    public IEnumerable<ItemForCollection> ItemsForCollection => _itemsForCollection;

    public event Action QuestCompleted;
    public abstract event Action<T> ItemCollected;

    public void Dispose()
    {
        CollectionPanel.ItemSelected -= OnItemSelected;
    }

    protected abstract void OnItemSelected(ItemForCollectionView selectdItemView, ItemForCollection selectedItemData);

    protected bool TryDeleteItem(ItemForCollectionView selectdItemView, ItemForCollection selectedItemData)
    {
        if (_itemsForCollection.Contains(selectedItemData))
        {
            _itemsForCollection.Remove(selectedItemData);
            CollectionPanel.Delete(selectdItemView, selectdItemView.Data);

            if (_itemsForCollection.Count == 0)
            {
                CollectionPanel.ItemSelected -= OnItemSelected;
                QuestCompleted?.Invoke();
            }

            return true;
        }

        return false;
    }
}
