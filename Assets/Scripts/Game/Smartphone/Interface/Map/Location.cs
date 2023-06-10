using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Location : IDisposable
{
    private BackgroundView _background;
    private CollectionPanel _collectionPanel;

    [SerializeField] private Sprite _backgroundSprite;
    [SerializeField] private List<ItemForCollection> _itemsForCollection;

    [field: SerializeField] public LocationType LocationType { get; private set; }
    [field: SerializeField] public string Name { get; private set; }

    public void Dispose()
    {
        _collectionPanel.OnItemDeleted -= OnItemDelete;
    }

    public void Initialize(BackgroundView background, CollectionPanel collectionPanel)
    {
        _background = background;
        _collectionPanel = collectionPanel;
    }

    public void Show()
    {
        _collectionPanel.HideItems();
        _background.Replace(_backgroundSprite);

        _background.OnPicturChanged += OnPicturChange;
        _collectionPanel.OnItemDeleted += OnItemDelete;
    }

    private void OnItemDelete(ItemForCollection itemData)
    {
        _itemsForCollection.Remove(itemData);
    }

    private void OnPicturChange()
    {
        _background.OnPicturChanged -= OnPicturChange;
        _collectionPanel.ShowItems(_itemsForCollection);
    }
}
