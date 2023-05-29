using UnityEngine;

public class QuizModel : XnodeModel
{
    [field: SerializeField] public CharacterType CharacterType { get; private set; }
    [field: SerializeField] public int PointsGoal { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}
