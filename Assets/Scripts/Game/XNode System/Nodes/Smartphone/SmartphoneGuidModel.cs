public class SmartphoneGuidModel : XnodeModel
{
    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}