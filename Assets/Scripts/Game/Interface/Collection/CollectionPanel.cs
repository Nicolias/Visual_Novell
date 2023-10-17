using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPanel : MonoBehaviour
{
    private ItemForCollection _currentSelectedItem;

    private List<ItemForCollectionView> _itemsOnPanel = new();

    public event Action<ItemForCollectionView, ItemForCollection> ItemSelected;
    public event Action<ItemForCollection> ItemDeleted;

    public void ShowItems(List<ItemForCollection> itemsForCollection)
    {
        foreach (var itemForCollection in itemsForCollection)
        {
            ItemForCollectionView itemView = Instantiate(itemForCollection.Prefab, transform);
            itemView.Initialize(itemForCollection);
            itemView.transform.localPosition = itemForCollection.ItemAfterInstantiatePosition;
            itemView.ItemSelected += OnItemSelected;
            _itemsOnPanel.Add(itemView);
        }
    }

    public void HideItems()
    {
        foreach (var item in _itemsOnPanel)
            Destroy(item.gameObject);

        _itemsOnPanel.RemoveAll(x => x);
    }

    public void Delete(ItemForCollectionView itemView, ItemForCollection itemData)
    {
        if (_currentSelectedItem == null | _currentSelectedItem != itemData)
            throw new InvalidProgramException("Сначало предмет должен быть выделен");

        Destroy(itemView.gameObject);
        _itemsOnPanel.Remove(itemView);

        ItemDeleted?.Invoke(itemData);
    }

    private void OnItemSelected(ItemForCollectionView itemForCollectionView, ItemForCollection itemData)
    {
        _currentSelectedItem = itemData;

        ItemSelected?.Invoke(itemForCollectionView, itemData);
    }
}