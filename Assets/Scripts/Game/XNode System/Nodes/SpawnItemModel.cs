using UnityEngine;

public class SpawnItemModel : XnodeModel
{
    [field: SerializeField] public Item Item { get; private set; }
    [field: SerializeField] public LocationType Location { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}