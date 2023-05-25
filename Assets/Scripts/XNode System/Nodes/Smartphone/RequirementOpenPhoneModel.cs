public class RequirementOpenPhoneModel : XnodeModel
{
    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}
