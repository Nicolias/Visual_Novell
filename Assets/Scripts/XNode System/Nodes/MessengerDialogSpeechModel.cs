using UnityEngine;

public class MessengerDialogSpeechModel : DialogSpeechModel, ISpeechModel
{
    [field: SerializeField] public MessegeSenderType MessegeSenderType { get; private set; }
}
