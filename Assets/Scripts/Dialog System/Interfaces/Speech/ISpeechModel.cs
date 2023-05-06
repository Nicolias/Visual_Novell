using UnityEngine;

public interface ISpeechModel
{
    public string SpeakerName { get; }
    public string Text { get; }
    public Sprite Avatar { get; }
}
