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

    public event Action Completed;

    public void Execute()
    {
        if (_model.itemForInventory is IUseableItemForInventory)
            _inventory.AddItemToInventory(_model.itemForInventory as IUseableItemForInventory);
        else
            _inventory.AddItemToInventory(_model.itemForInventory);
        
        Completed?.Invoke();
    }
}
