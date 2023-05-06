using XNode;

public interface IChoiceModel
{
    public string[] Choices { get; }
    public Node[] Nodes { get; }
    public string QuestionText { get; }

    public void SetEndPort(Node node);
}
