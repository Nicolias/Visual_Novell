using UnityEngine;

public class DialogSpeechModel : XnodeModel, ISpeechModel
{
    [SerializeField] private bool _isImmediatelyNextNode;

    [SerializeField] private Sprite _speakerAvatar;
	[SerializeField] private string _speakerName;
	[SerializeField, TextArea(5, 10)] private string _speechText;

    public Sprite Avatar => _speakerAvatar;
	public string SpeakerName => _speakerName;
    public string Text => _speechText;
    public bool IsImmediatelyNextNode => _isImmediatelyNextNode;

    public void TryReplaceNickname(StaticData staticData)
    {
        _speechText = _speechText.Replace(staticData.SpecWordForNickName, staticData.Nickname);
        _speakerName = _speakerName.Replace(staticData.SpecWordForNickName, staticData.Nickname);
    }
}
