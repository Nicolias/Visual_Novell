using UnityEngine;

public class ShowGuidModel : XnodeModel
{
    [field: SerializeField] public Sprite GuidSprite { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}