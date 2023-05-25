using UnityEngine;

public class AudioModel : XnodeModel
{
    [field : SerializeField] public AudioClip AudioClip { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}
