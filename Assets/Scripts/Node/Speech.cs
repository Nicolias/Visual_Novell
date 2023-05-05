using UnityEngine;
using XNode;

public class Speech : Node 
{
	[Input, SerializeField] private bool _input;
	[Output, SerializeField] private bool _outPut;

	[SerializeField] private string _speakerName;
	[SerializeField, TextArea(5, 10)] private string _speechText;

	[Output(dynamicPortList = true), SerializeField] private string[] _choise;

	public string[] Choise => _choise;

	public string SpeakerName => _speakerName;
	public string Text => _speechText;

	protected override void Init() 
	{
		base.Init();
	}

	public override object GetValue(NodePort port) 
	{
		return null;
	}

}