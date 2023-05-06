using System;

public interface ISpeechView
{
    public ShowTextStatus ShowStatus { get; }

    public event Action OnClick;
    public void ShowSmooth(string name, string text);
    public void Show(string name, string text);
    public void OnCallBack();
    public void Hide();
}