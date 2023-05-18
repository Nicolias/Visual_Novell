using UnityEngine;
using XNode;

public class ChangeDialogDataModel : XnodeModel
{
    [field: SerializeField] public NodeGraph NodeGraph { get; private set; }
}