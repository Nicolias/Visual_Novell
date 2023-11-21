public interface IInventory
{
    public Inventory InventoryComponent { get; }

    public void AddItemToInventory(IItemForInventory itemData);
}
