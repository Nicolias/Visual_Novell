using System;
using UnityEngine;

[Serializable]
public class DialogSpeechForMeeting : IDialogSpeechModel
{
    [field: SerializeField]public Sprite Avatar { get; private set; }

    [field: SerializeField]public bool IsImmediatelyNextNode { get; private set; }

    [field: SerializeField] public string Text { get; private set; }

    [field: SerializeField] public string SpeakerName { get; private set; }

    public void Initialize(StaticData staticData)
    {   
    }
}