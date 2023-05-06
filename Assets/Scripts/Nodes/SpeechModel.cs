using System;
using UnityEngine;
using static XNode.Node;

public class SpeechModel : XnodeModel, ISpeechModel
{
	[SerializeField] private string _speakerName;
	[SerializeField, TextArea(5, 10)] private string _speechText;

	public string SpeakerName => _speakerName;
	public string Text => _speechText;
}
