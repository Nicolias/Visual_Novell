using UnityEngine;

public class WaitForSecondsModel : XnodeModel
{
    [field: SerializeField] public float Seconds { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}
