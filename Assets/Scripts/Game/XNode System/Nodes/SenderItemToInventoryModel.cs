using UnityEngine;

public class SenderItemToInventoryModel : XnodeModel
{
    [field: SerializeField] public ItemForInventory itemForInventory { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}