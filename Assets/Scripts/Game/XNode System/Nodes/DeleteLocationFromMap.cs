using System.Collections.Generic;
using UnityEngine;

public class DeleteLocationFromMap : XnodeModel
{
    [field: SerializeField] public List<Location> Locations { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}