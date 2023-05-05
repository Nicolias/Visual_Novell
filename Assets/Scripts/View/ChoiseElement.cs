using TMPro;
using XNode;

public class ChoiseElement
{
    public string Text { get; private set; }

    public Node Node { get; private set; }

    public ChoiseElement(string text, Node node)
    {
        Text = text;
        Node = node;
    }
}