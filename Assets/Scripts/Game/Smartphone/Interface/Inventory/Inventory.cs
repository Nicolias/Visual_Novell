using System;
using System.Collections.Generic;
using SaveData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class Inventory : MonoBehaviour, ISaveLoadObject, IInventory
{
    [Inject] private SaveLoadServise _saveLoadServise;
    [Inject] private Battery _battery;

    [SerializeField] private InventorySaveLoader _inventorySaveLoader;

    [SerializeField] private Transform _itemCellsContainer;
    [SerializeField] private InventoryCellView _inventoryCell;

    [SerializeField] private Button _openButton, _closeButton;

    [SerializeField] private TMP_Text _itemDiscription;
    [SerializeField] private Image _itamImage;
    [SerializeField] private Button _useEffectButton;
    [SerializeField] private TMP_Text _usableCountText;
 
    private Canvas _selfCanvas;
    
    private List<InventoryCell> _items = new List<InventoryCell>();
    private List<UsableInventoryCell> _useableItems = new List<UsableInventoryCell>();

    private const string _saveKey = "InventorySave";

    public Inventory InventoryComponent => this;
    
    public IEnumerable<InventoryCell> Items => _items;
    public IEnumerable<UsableInventoryCell> UseableItems => _useableItems;

    private void OnValidate()
    {
        _inventorySaveLoader.Validate();
    }

    private void Awake()
    {
        _selfCanvas = GetComponent<Canvas>();

        _inventorySaveLoader.Construct(this, _saveLoadServise);
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
        _itamImage.color = new Color(1, 1, 1, 0);
        _itemDiscription.text = "";
    }

    public void Hide()
    {
        _selfCanvas.enabled = false;
    }

    public void AddItemToInventory(IItemForInventory itemData)
    {
        InventoryCellView cellView = CreateItemView(itemData);
        var item = new InventoryCell(itemData, cellView);
        
        _items.Add(item);   
    }

    public void AddItemToInventory(IUseableItemForInventory itemData)
    {
        InventoryCellView cellView = CreateItemView(itemData);
        var item = new UsableInventoryCell(itemData, cellView, _useEffectButton, _usableCountText, _battery);
        
        _useableItems.Add(item);
    }

    private InventoryCellView CreateItemView(IItemForInventory itemData)
    {
        InventoryCellView cellView = Instantiate(_inventoryCell, _itemCellsContainer);
        cellView.Initialize(_itamImage, _itemDiscription);
        
        return cellView;
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new BoolData() { Bool = enabled });
        _inventorySaveLoader.Save();
    }

    public void Load()
    {
        enabled = _saveLoadServise.Load<BoolData>(_saveKey).Bool;
        _inventorySaveLoader.Load();
    }
}

[Serializable]
public class InventorySaveLoader : ISaveLoadObject
{
    private IInventory _inventory;
    private SaveLoadServise _saveLoaderServise;

    [SerializeField] private List<AbstractItem> _items;
    
    private string _saveKey = "InventorySave";


    [Inject]
    public void Construct(IInventory inventory, SaveLoadServise saveLoadServise)
    {
        _inventory = inventory;
        _saveLoaderServise = saveLoadServise;
    }

    public void Validate()
    {
        _items.RemoveAll(item => item is IItemForInventory == false);
    }

    public void Save()
    {
        List<InventoryCell> itemCells = new List<InventoryCell>(_inventory.Items);
        List<UsableInventoryCell> useableItemCells = new List<UsableInventoryCell>(_inventory.UseableItems);

        List<IItemForInventory> items = GetItemForInventories<IItemForInventory>();
        List<IUseableItemForInventory> useableItems = GetItemForInventories<IUseableItemForInventory>();

        _saveLoaderServise.Save(_saveKey + "Count", new IntData() {Int = itemCells.Count});
        _saveLoaderServise.Save(_saveKey + "UseCount", new IntData() {Int = useableItemCells.Count});

        for (int i = 0; i < itemCells.Count; i++)
            _saveLoaderServise.Save(_saveKey + i, new IntData() { Int = itemCells.FindIndex(cell => cell.Data == items[i]) });

        for (int i = 0; i < useableItemCells.Count; i++)
        {
            var cell = useableItemCells.Find(cell => cell.Data.ID == useableItems[i].ID);

            _saveLoaderServise.Save(_saveKey + 1 + i, new SaveData.UsableInventoryItem()
            {
                DataIndex = useableItemCells.FindIndex(cells => cells == cell),
                UseCount = cell.UsableCount
            });
        }
    }

    public void Load()
    {
        List<IItemForInventory> item = GetItemForInventories<IItemForInventory>();
        List<IUseableItemForInventory> useableItems = GetItemForInventories<IUseableItemForInventory>();

        for (int i = 0; i < _saveLoaderServise.Load<IntData>(_saveKey + "Count").Int; i++)
        {
            _inventory.AddItemToInventory(item[_saveLoaderServise.Load<IntData>(_saveKey + i).Int]);    
        }

        for (int i = 0; i < _saveLoaderServise.Load<IntData>(_saveKey + "UseCount").Int; i++)
        {
            var data = _saveLoaderServise.Load<SaveData.UsableInventoryItem>(_saveKey + 1 + i);
            IUseableItemForInventory useableItemForInventory = useableItems[data.DataIndex];
            _inventory.AddItemToInventory(new UsableInventoryItem(useableItemForInventory, data.UseCount));
        }
    }
    
    private List<T> GetItemForInventories<T>() where T : class, IItemForInventory
    {
        List<T> itemsForInventorie = new List<T>();

        foreach (var item in _items)
            if(item is T)
                itemsForInventorie.Add(item as T);

        return itemsForInventorie;
    }
    
    public class UsableInventoryItem : IUseableItemForInventory
    {
        private IUseableItemForInventory _item;

        public UsableInventoryItem(IUseableItemForInventory item, int usableCount)
        {
            _item = item;
            UseCount = usableCount;
        }

        public Sprite ItemSpriteInInventory => _item.ItemSpriteInInventory;

        public string Name => _item.Name;

        public string Discription => _item.Discription;

        public bool HaveEffect => _item.HaveEffect;

        public int UseCount { get; private set; }

        public int ID => _item.ID;

        public void Accept(UsableInventoryCell inventoryCell)
        {
            _item.Accept(inventoryCell);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}