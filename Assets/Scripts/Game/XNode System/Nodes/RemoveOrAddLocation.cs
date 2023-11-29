using System.Collections.Generic;
using UnityEngine;

public class RemoveOrAddLocation : XnodeModel
{
    [field: SerializeField] public List<LocationSO> LocationsForAdd { get; private set; }
    [field: SerializeField] public List<LocationSO> LocationsForRemove { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}