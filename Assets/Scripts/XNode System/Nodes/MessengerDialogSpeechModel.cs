using UnityEngine;

public class MessengerDialogSpeechModel : DialogSpeechModel, ISpeechModel
{
    [field: SerializeField] public MessegeSenderType MessegeSenderType { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}
