using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class Inventory : MonoBehaviour, ISaveLoadObject
{
    [Inject] private SaveLoadServise _saveLoadServise;

    [Inject] private DiContainer _di;

    [SerializeField] private Transform _itemCellsContainer;
    [SerializeField] private InventoryCell _inventoryCell;

    [SerializeField] private Button _openButton, _closeButton;

    [SerializeField] private TMP_Text _itemDiscription;
    [SerializeField] private Image _itamImage;

    private Canvas _selfCanvas;
    private List<ItemForCollection> _items = new List<ItemForCollection>();

    private const string _saveKey = "InventorySave";

    private void Awake()
    {
        _selfCanvas = GetComponent<Canvas>();
        Add();
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

    public void AddItemToInventory(ItemForCollection itemData)
    {
        if (itemData == null) throw new InvalidOperationException();

        _items.Add(itemData);

        var itemCell = _di.InstantiatePrefabForComponent<InventoryCell>(_inventoryCell, _itemCellsContainer);
        itemCell.Initialize(itemData, _itamImage, _itemDiscription);
    }

    public void Add()
    {
        _saveLoadServise.Add(this);
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
