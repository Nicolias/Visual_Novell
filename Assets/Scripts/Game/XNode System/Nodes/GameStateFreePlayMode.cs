public class GameStateFreePlayMode : XnodeModel
{
    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}