using XNode;

public interface IChoiceModel
{
    public string[] Choices { get; }
    public Node[] Nodes { get; }

    public void SetEndPort(Node node);
}
