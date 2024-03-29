﻿using UnityEngine;

public class SpawnItemModel : XnodeModel
{
    [field: SerializeField] public Item Item { get; private set; }
    [field: SerializeField] public LocationSO Location { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}