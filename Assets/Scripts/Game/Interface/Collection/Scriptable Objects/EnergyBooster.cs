using UnityEngine;

[CreateAssetMenu(fileName = "Energy booster", menuName = "Items")]
public class EnergyBooster : ItemForInventory, IUseableItemForInventory
{
    [field:SerializeField] public int BoostValue { get; private set; }
    [field: SerializeField] public int UseCount { get; private set; }

    public void Accept(UsableInventoryCell inventoryCell)
    {
        inventoryCell.Visit(this);
    }
}