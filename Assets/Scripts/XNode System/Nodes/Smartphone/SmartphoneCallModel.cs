using UnityEngine;
using XNode;

public class SmartphoneCallModel : XnodeModel, ICallModel
{
    [SerializeField] private string _characterName;

    [SerializeField]
    [Output(dynamicPortList = true)]
    private Sprite[] _images;

    public Sprite[] Images => _images;
    public Node[] Nodes => GetNodes();
    public string CharacterName => _characterName;

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

    private Node[] GetNodes()
    {
        Node[] result = new Node[_images.Length];

        for (int i = 0; i < _images.Length; i++)
            result[i] = GetPort($"{nameof(_images)} {i}").Connection.node;

        return result;
    }
}