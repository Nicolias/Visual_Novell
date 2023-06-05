using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class Inventory : MonoBehaviour
{
    [Inject] private CollectionPanel _collectionPanel;
    [Inject] private DiContainer _di;

    [SerializeField] private Transform _itemCellsContainer;
    [SerializeField] private InventoryCell _inventoryCell;

    [SerializeField] private Button _openButton, _closeButton;

    private Canvas _selfCanvas;
    private List<ItemForCollection> _items = new();

    private void Awake()
    {
        _selfCanvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        _openButton.onClick.AddListener(Show);
        _closeButton.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        _selfCanvas.enabled = true;
    }

    public void Hide()
    {
        _selfCanvas.enabled = false;
    }

    public void AddItemToInventory(ItemForCollection itemData)
    {
        if (itemData == null) throw new InvalidOperationException();

        _items.Add(itemData);

        var itemCell = _di.InstantiatePrefabForComponent<InventoryCell>(_inventoryCell, _itemCellsContainer);
        itemCell.Initialize(itemData);
    }
}
