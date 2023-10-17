public interface IInventory
{
    public Inventory InventoryComponent { get; }

    public void AddItemToInventory(ItemForCollection itemData);
}
