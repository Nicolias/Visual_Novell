using UnityEngine;

public class ChangeMapEnabledModel : XnodeModel
{
    [field: SerializeField] public bool Enebled { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}