using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPanel : MonoBehaviour
{
    private ItemForCollection _currentSelectedItem;

    private List<ItemForCollectionView> _itemsOnPanel = new();

    public event Action<ItemForCollectionView, ItemForCollection> ItemSelected;
    public event Action<ItemForCollection> ItemDeleted;

    public List<ItemForCollectionView> CreateItemsView(IEnumerable<ItemForCollection> itemsData)
    {
        List<ItemForCollectionView> itemsView = new List<ItemForCollectionView>();

        foreach (var itemData in itemsData)
        {
            ItemForCollectionView itemView = CreateItemsView(itemData, itemData.ItemAfterInstantiatePosition);
            itemsView.Add(itemView);
        }

        return itemsView;
    }

    public ItemForCollectionView CreateItemsView(ItemForCollection itemData, Vector2 itemSpawnPosition)
    {
        ItemForCollectionView itemView = Instantiate(itemData.Prefab, transform);
        itemView.Initialize(itemData);
        itemView.transform.localPosition = itemSpawnPosition;
        itemView.gameObject.SetActive(false);

        return itemView;
    }

    public void Delete(ItemForCollectionView itemView, ItemForCollection itemData)
    {
        if (_currentSelectedItem == null | _currentSelectedItem != itemData)
            throw new InvalidProgramException("Сначало предмет должен быть выделен");

        itemView.ItemSelected -= OnItemSelected;
        _itemsOnPanel.Remove(itemView);
        DestroyImmediate(itemView.gameObject);

        ItemDeleted?.Invoke(itemData);
    }

    public void ShowItems(IEnumerable<ItemForCollectionView> itemsView)
    {
        HideItems();

        foreach (var itemView in itemsView)
        {
            itemView.gameObject.SetActive(true);
            itemView.ItemSelected += OnItemSelected;
            _itemsOnPanel.Add(itemView);
        }
    }

    public void HideItems()
    {
        foreach (var item in _itemsOnPanel)
        {
            item.gameObject.SetActive(false);
            item.ItemSelected -= OnItemSelected;
        }

        _itemsOnPanel.RemoveAll(x => x);
    }

    private void OnItemSelected(ItemForCollectionView itemForCollectionView, ItemForCollection itemData)
    {
        _currentSelectedItem = itemData;

        ItemSelected?.Invoke(itemForCollectionView, itemData);
    }
}