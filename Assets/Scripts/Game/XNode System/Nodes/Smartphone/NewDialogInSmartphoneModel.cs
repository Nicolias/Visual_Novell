using System.Collections.Generic;
using UnityEngine;
using XNode;

public class NewDialogInSmartphoneModel : XnodeModel
{
    [field: SerializeField] public List<MessegeData> NewDialogs { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}
