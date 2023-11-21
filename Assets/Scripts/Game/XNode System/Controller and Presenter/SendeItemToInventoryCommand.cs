using System;

public class SendeItemToInventoryCommand : ICommand
{
    private readonly SenderItemToInventoryModel _model;
    private IInventory _inventory;

    public SendeItemToInventoryCommand(IInventory inventory, SenderItemToInventoryModel model)
    {
        _model = model;
        _inventory = inventory;
    }

    public event Action Complete;

    public void Execute()
    {
        _inventory.AddItemToInventory(_model.itemForInventory);
        Complete?.Invoke();
    }
}