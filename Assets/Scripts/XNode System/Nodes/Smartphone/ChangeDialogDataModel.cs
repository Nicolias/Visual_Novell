using UnityEngine;
using XNode;

public class ChangeDialogDataModel : XnodeModel
{
    [field: SerializeField] public NodeGraph NodeGraph { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}