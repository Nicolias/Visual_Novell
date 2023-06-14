using System;
using UnityEngine;

public class SetTimeOnSmartphoneWatchModel : XnodeModel
{
    [field : SerializeField] public int Hour { get; private set; }
    [field : SerializeField] public int Minute { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}
