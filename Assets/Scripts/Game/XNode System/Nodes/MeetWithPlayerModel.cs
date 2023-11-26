using UnityEngine;

public class MeetWithPlayerModel : XnodeModel
{
    [field: SerializeField] public CharacterType CharacterType { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}