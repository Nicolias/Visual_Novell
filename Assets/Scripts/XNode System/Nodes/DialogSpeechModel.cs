using UnityEngine;

public class DialogSpeechModel : XnodeModel, ISpeechModel
{
    [SerializeField] private bool _isImmediatelyNextNode;

    [SerializeField] private Sprite _speakerAvatar;
	[SerializeField] private string _speakerName = "";
	[SerializeField, TextArea(5, 10)] private string _speechText;
    private StaticData _staticData;

    public Sprite Avatar => _speakerAvatar;
    public bool IsImmediatelyNextNode => _isImmediatelyNextNode;

    public string Text
    {
        get
        {
            return _speechText.Replace(_staticData.SpecWordForNickName, _staticData.Nickname);
        }
    }

    public string SpeakerName
    {
        get
        {

            return _speakerName.Replace(_staticData.SpecWordForNickName, _staticData.Nickname);
        }
    }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Initialize(StaticData staticData)
    {
        _staticData = staticData;
    }
}
