public interface ISpeechModel
{
    public string Text { get; }
    public void TryReplaceNickname(string cpecWord, string nickname);
}
