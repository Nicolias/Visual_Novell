using UnityEngine;
using XNode;

public class ContinueStoryModel : XnodeModel
{
    [Output, SerializeField] private string _nextStoryNode;
    [Output, SerializeField] private string _endGame;

    public Node NextStoryNode => GetPort($"{nameof(_nextStoryNode)}").Connection.node;
    public Node EndGame => GetPort($"{nameof(_endGame)}").Connection.node;

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void SetEndPort(Node node)
    {
        NodePort node1 = GetOutputPort("_outPut");
        NodePort node2 = node.GetInputPort("_inPut");
        node1.ClearConnections();
        node1.Connect(node2);
    }
}