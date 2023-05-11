using UnityEngine;
using XNode;

public class XnodeModel : Node
{
    [Input, SerializeField] private bool _inPut;
    [Output, SerializeField] private bool _outPut;
}
