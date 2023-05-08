using UnityEngine;
using Zenject;

public class SpeechModel : XnodeModel, ISpeechModel
{
	[SerializeField] private Sprite _speakerAvatar;
	[SerializeField] private string _speakerName;
	[SerializeField, TextArea(5, 10)] private string _speechText;

    public Sprite Avatar => _speakerAvatar;
	public string SpeakerName => _speakerName;
    public string Text => _speechText;
}
