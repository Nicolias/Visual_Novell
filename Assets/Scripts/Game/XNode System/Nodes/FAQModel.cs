
public class FAQModel : ChoiceModel, IChoiceModel
{
    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}