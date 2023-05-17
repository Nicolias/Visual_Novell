﻿using UnityEngine;

public class MonologSpeechModel : XnodeModel, ISpeechModel
{
    [SerializeField] private bool _isImmediatelyNextNode;
    [SerializeField, TextArea(5, 10)] private string _speechText;

    public string Text => _speechText;
    public bool IsImmediatelyNextNode => _isImmediatelyNextNode;

    public void TryReplaceNickname(StaticData staticData)
    {
        _speechText = _speechText.Replace(staticData.SpecWordForNickName, staticData.Nickname);
    }
}