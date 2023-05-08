
using XNode;

public interface ISelectionActModel
{
    public string[] Choices { get; }
    public Node[] Nodes { get; }

    public void SetEndPort(Node node);
}

