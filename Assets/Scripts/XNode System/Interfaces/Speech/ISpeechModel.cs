public interface ISpeechModel
{
    public bool IsImmediatelyNextNode { get; }
    public string Text { get; }
    public void TryReplaceNickname(string cpecWord, string nickname);
}
