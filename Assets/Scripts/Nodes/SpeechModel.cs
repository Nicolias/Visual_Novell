using UnityEngine;

public class SpeechModel : XnodeModel, ISpeechModel
{
	[SerializeField] private string _speakerName;
	[SerializeField, TextArea(5, 10)] private string _speechText;

	public string SpeakerName => _speakerName;
	public string Text => _speechText;
}