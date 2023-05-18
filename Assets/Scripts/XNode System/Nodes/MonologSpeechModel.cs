using UnityEngine;

public class MonologSpeechModel : XnodeModel, ISpeechModel
{
    [SerializeField] private bool _isImmediatelyNextNode;
    [SerializeField, TextArea(5, 10)] private string _speechText;
    private StaticData _staticData;

    public string Text
    {
        get
        {
            return _speechText.Replace(_staticData.SpecWordForNickName, _staticData.Nickname);
        }
    }
    public string SpeakerName => throw new System.NotImplementedException();
    public bool IsImmediatelyNextNode => _isImmediatelyNextNode;


    public void Initialize(StaticData staticData)
    {
        _staticData = staticData;
    }
}
