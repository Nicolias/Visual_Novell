public interface ISpeechModel
{
    public bool IsImmediatelyNextNode { get; }
    public string Text { get; }
    public string SpeakerName { get; }
    public void Initialize(StaticData staticData);
}
