using System.Collections.Generic;

public interface IInventory
{
    public Inventory InventoryComponent { get; }

    public IEnumerable<InventoryCell> Items { get; }
    public IEnumerable<UsableInventoryCell> UseableItems { get; }

    public void AddItemToInventory(IItemForInventory itemData);

    public void AddItemToInventory(IUseableItemForInventory useableItemForInventory);
}
