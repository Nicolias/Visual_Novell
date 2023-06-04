using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPanel : MonoBehaviour
{
    public event Action<ItemForCollection> OnItemDeleted;

    private List<ItemForCollectionView> _itemsOnPanel = new();

    public void ShowItems(List<ItemForCollection> itemsForCollection)
    {
        foreach (var itemForCollection in itemsForCollection)
        {
            var itemView = Instantiate(itemForCollection.Prefab, transform);
            itemView.Initialize(itemForCollection);
            itemView.transform.localPosition = itemForCollection.ItemAfterInstantiatePosition;
            itemView.OnItemSelected += OnItemSelected;
            _itemsOnPanel.Add(itemView);
        }
    }

    public void HideItems()
    {
        foreach (var item in _itemsOnPanel)
            Destroy(item.gameObject);

        _itemsOnPanel.RemoveAll(x => x);
    }
    private void OnItemSelected(ItemForCollectionView itemForCollectionView, ItemForCollection itemData)
    {
        Destroy(itemForCollectionView.gameObject);
        _itemsOnPanel.Remove(itemForCollectionView);
        OnItemDeleted?.Invoke(itemData);
    }
}
