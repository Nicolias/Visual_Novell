using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPanel : MonoBehaviour
{
    private ItemForCollection _currentSelectedItem;

    private List<ItemForCollectionView> _itemsOnPanel = new();

    public event Action<ItemForCollectionView, ItemForCollection> ItemSelected;
    public event Action<ItemForCollection> ItemDeleted;

    public void ShowItems(List<ItemForCollectionView> itemsView)
    {
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

    public List<ItemForCollectionView> CreateItemsView(List<ItemForCollection> itemsData)
    {
        List<ItemForCollectionView> itemsView = new List<ItemForCollectionView>();

        foreach (var itemData in itemsData)
        {
            ItemForCollectionView itemView = Instantiate(itemData.Prefab, transform);
            itemView.Initialize(itemData);
            itemView.transform.localPosition = itemData.ItemAfterInstantiatePosition;
            itemView.gameObject.SetActive(false);

            itemsView.Add(itemView);
        }

        return itemsView;
    }

    public void Delete(ItemForCollectionView itemView, ItemForCollection itemData)
    {
        if (_currentSelectedItem == null | _currentSelectedItem != itemData)
            throw new InvalidProgramException("Сначало предмет должен быть выделен");

        DestroyImmediate(itemView.gameObject);
        _itemsOnPanel.Remove(itemView);
        itemView.ItemSelected -= OnItemSelected;

        ItemDeleted?.Invoke(itemData);
    }

    private void OnItemSelected(ItemForCollectionView itemForCollectionView, ItemForCollection itemData)
    {
        _currentSelectedItem = itemData;

        ItemSelected?.Invoke(itemForCollectionView, itemData);
    }
}