using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class ItemForCollectionView : MonoBehaviour
{
    private Image _itemImage;

    private ItemForCollection _itemData;
    private Button _selfButton;

    public ItemForCollection Data => _itemData;

    public event Action<ItemForCollectionView, ItemForCollection> ItemSelected;

    private void Awake()
    {
        _selfButton = GetComponent<Button>();
        _itemImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(() =>
        {
            if(_itemData.IsInteractable)
                ItemSelected?.Invoke(this, _itemData);
        });
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
    }

    public void Initialize(ItemForCollection itemData)
    {
        _itemData = itemData;

        if(_itemImage != null)
            _itemImage.sprite = itemData.ItemSpriteInScene;
    }

    public void SelecteItem()
    {
        ItemSelected?.Invoke(this, _itemData);
    }
}
