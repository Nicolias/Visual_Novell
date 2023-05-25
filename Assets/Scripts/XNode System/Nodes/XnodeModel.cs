using UnityEngine;
using XNode;

public abstract class XnodeModel : Node
{
    [Input, SerializeField] private bool _inPut;
    [Output, SerializeField] private bool _outPut;

    public abstract void Accept(ICommanderVisitor visitor);
}
