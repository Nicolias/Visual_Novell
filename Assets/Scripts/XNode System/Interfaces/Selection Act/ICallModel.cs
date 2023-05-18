using UnityEngine;
using XNode;

public interface ICallModel 
{
    public Sprite[] Images { get; }
    public Node[] Nodes { get; }
    public string CharacterName { get; }

    public void SetEndPort(Node node);

}