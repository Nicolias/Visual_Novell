using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventoryCellView : CellView
{
    private TMP_Text _itemDiscription;
    private Image _itemImage;

    public void Initialize(Image itemImage, TMP_Text itemDiscription)
    {
        _itemImage = itemImage;
        _itemDiscription = itemDiscription;
    }

    public void DisplayItemInformation(IItemForInventory data)
    {
        _itemImage.sprite = data.ItemSpriteInInventory;
        _itemDiscription.text = data.Discription;

        _itemImage.color = new Color(1, 1, 1, 1);
    }    

    public void Hide()
    {
        _itemImage.sprite = null;
        _itemDiscription.text = "";

        _itemImage.color = new(1, 1, 1, 0);
    }
}

public class InventoryCell : AbstractCell<IItemForInventory>
{
    private InventoryCellView _cellView;

    public InventoryCell(IItemForInventory data, InventoryCellView cellView) : base(data, cellView)
    {
        _cellView = cellView;
    }

    protected override void OnCellClicked()
    {
        _cellView.DisplayItemInformation(Data);
    }
}

public class UsableInventoryCell : AbstractCell<IUseableItemForInventory>
{
    private readonly InventoryCellView _cellView;
    private readonly Battery _battery;

    private readonly Button _useEffectButton;
    private readonly TMP_Text _useCountText;

    private int _usableCount;

    public UsableInventoryCell(IUseableItemForInventory data, InventoryCellView cellView, Button useEffectButton, TMP_Text useCountText, Battery battery) : base(data, cellView)
    {
        _battery = battery;

        _cellView = cellView;
        _useEffectButton = useEffectButton;
        _useCountText = useCountText;

        _usableCount = data.UseCount;
    }

    public int UsableCount => _usableCount;

    public override void Dispose()
    {
        base.Dispose();

        _useEffectButton.gameObject.SetActive(false);
        _useCountText.text = "";
        _useEffectButton.onClick.RemoveAllListeners();
    }

    public void Add(int useCount)
    {
        if (useCount <= 0)
            throw new InvalidOperationException();

        _usableCount += useCount;
    }

    public void Visit(EnergyBooster energyBooster)
    {
        _battery.Accure(energyBooster.BoostValue);
    }

    protected override void OnCellClicked()
    {
        _cellView.DisplayItemInformation(Data);
        _useCountText.text = "Осталось использований: " + _usableCount.ToString();
        _useEffectButton.gameObject.SetActive(true);
        _useEffectButton.onClick.AddListener(OnClickedUseEffectButton);
    }

    private void OnClickedUseEffectButton()
    {
        Data.Accept(this);
        _usableCount--;
        _useCountText.text = _useCountText.text = "Осталось использований: " + _usableCount.ToString();

        if (_usableCount == 0)
        {
            _cellView.Hide();
            _useEffectButton.gameObject.SetActive(false);
            _useCountText.text = "";
            _cellView.Destory();
        }

        if (_usableCount < 0)
            throw new InvalidOperationException();
    }
}