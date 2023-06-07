using TMPro;
using UnityEngine;
using XNode;

public class NameInputModel : XnodeModel, INicknameInputModel
{
    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}