using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemForCollectionView : MonoBehaviour
{
    public event Action<ItemForCollectionView, ItemForCollection> OnItemSelected;

    [SerializeField] private Image _itemImage;

    private ItemForCollection _itemData;
    private Button _selfButton;

    public ItemForCollection Data => _itemData;

    private void Awake()
    {
        _selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(() => OnItemSelected?.Invoke(this, _itemData));
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
    }

    public void Initialize(ItemForCollection itemData)
    {
        _itemData = itemData;

        _itemImage.sprite = itemData.ItemSpriteInScene;
    }
}