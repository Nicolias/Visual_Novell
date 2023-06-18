using UnityEngine;

public class ChapterCaptionModel : XnodeModel
{
    [field: SerializeField] public string ChapterText { get; private set; } 

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}