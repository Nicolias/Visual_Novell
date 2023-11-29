using UnityEngine;
using XNode;

public class SetQuestOnLocationModel : XnodeModel
{
    [field: SerializeField] public Node Quest { get; private set; }
    [field: SerializeField] public LocationSO Location { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}