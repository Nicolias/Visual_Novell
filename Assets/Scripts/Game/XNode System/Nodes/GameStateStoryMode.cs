public class GameStateStoryMode : XnodeModel
{
    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}
