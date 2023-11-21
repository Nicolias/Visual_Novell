using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class Inventory : MonoBehaviour, ISaveLoadObject, IInventory
{
    [Inject] private SaveLoadServise _saveLoadServise;
    [Inject] private Battery _battery;
    [Inject] private DiContainer _di;

    [SerializeField] private Transform _itemCellsContainer;
    [SerializeField] private InventoryCellView _inventoryCell;

    [SerializeField] private Button _openButton, _closeButton;

    [SerializeField] private TMP_Text _itemDiscription;
    [SerializeField] private Image _itamImage;
    [SerializeField] private Button _useEffectButton;

    private Canvas _selfCanvas;
    private List<IItemForInventory> _items = new();

    private const string _saveKey = "InventorySave";

    public Inventory InventoryComponent => this;

    private void Awake()
    {
        _selfCanvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        _openButton.onClick.AddListener(Show);
        _closeButton.onClick.AddListener(Hide);

        if (_saveLoadServise.HasSave(_saveKey))
            Load();
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();

        Save();
    }

    public void Show()
    {
        _selfCanvas.enabled = true;
        _itamImage.color = new(1, 1, 1, 0);
        _itemDiscription.text = "";
    }

    public void Hide()
    {
        _selfCanvas.enabled = false;
    }

    public void AddItemToInventory(IItemForInventory itemData)
    {
        if (itemData == null) throw new InvalidOperationException();

        _items.Add(itemData);

        InventoryCellView cellView = Instantiate(_inventoryCell, _itemCellsContainer);
        cellView.Initialize(_itamImage, _itemDiscription);


        if (itemData is IUseableItemForInventory)
            new UsableInventoryCell(itemData as IUseableItemForInventory, cellView, _useEffectButton, _battery);
        else
            new InventoryCell(itemData, cellView);
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.BoolData() { Bool = enabled });
    }

    public void Load()
    {
        enabled = _saveLoadServise.Load<SaveData.BoolData>(_saveKey).Bool;
    }
}
