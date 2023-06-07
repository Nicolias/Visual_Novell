using UnityEngine;
using XNode;

public class NewDialogInSmartphoneModel : XnodeModel
{
    [field: SerializeField] public MessegeData NewDialog { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}
