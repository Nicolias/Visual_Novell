using System;

public interface ISpeechView
{
    public event Action OnClick;

    public ShowTextStatus ShowStatus { get; }
    public void ShowSmooth(string name, string text);
    public void Show(string name, string text);
    public void Hide();
}