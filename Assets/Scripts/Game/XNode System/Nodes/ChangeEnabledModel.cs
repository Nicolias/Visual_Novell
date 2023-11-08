using Dictionary;
using UnityEngine;

public class ChangeEnabledModel : XnodeModel
{
    [field: SerializeField] public Dictionary<SmartphoneWindows, bool> Windows { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}