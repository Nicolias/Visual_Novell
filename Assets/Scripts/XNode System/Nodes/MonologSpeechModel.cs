using UnityEngine;

public class MonologSpeechModel : XnodeModel, ISpeechModel
{
    [SerializeField, TextArea(5, 10)] private string _speechText;

    public string Text => _speechText;

    public void TryReplaceNickname(string cpecWord, string nickname)
    {
        _speechText = _speechText.Replace(cpecWord, nickname);
    }
}