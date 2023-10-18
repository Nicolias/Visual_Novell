using System.Collections.Generic;
using UnityEngine;

public class BackgroundModel : XnodeModel
{
    [field: SerializeField] public Sprite Sprite { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}
