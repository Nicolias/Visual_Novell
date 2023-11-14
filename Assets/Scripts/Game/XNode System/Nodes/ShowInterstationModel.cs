public class ShowInterstationModel : XnodeModel
{
    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}