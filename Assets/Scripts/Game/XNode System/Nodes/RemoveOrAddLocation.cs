using System.Collections.Generic;
using UnityEngine;

public class RemoveOrAddLocation : XnodeModel
{
    [field: SerializeField] public List<Location> LocationsForAdd { get; private set; }
    [field: SerializeField] public List<Location> LocationsForRemove { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}