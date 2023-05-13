using UnityEngine;
using XNode;

public class NewDialogInSmartphoneModel : XnodeModel
{
    [field: SerializeField] public NodeGraph NewDialog { get; private set; }
}
