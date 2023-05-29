using UnityEngine;

public class MiniGameModel : XnodeModel
{
    [field: SerializeField] public CharacterType CharacterType { get; private set; }
    [field: SerializeField] public int BettaryChargeLevelCondition { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}