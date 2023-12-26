using UnityEngine;

public class ChangeLocationModel : XnodeModel
{
    [field: SerializeField] public LocationSO Location { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}
