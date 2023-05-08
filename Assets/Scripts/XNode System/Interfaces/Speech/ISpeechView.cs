using System;
using UnityEngine;

public interface ISpeechView
{
    public event Action OnClick;

    public ShowTextStatus ShowStatus { get; }
    public void ShowSmooth(string name, string text, Sprite speakerAvatar);
    public void Show(string name, string text, Sprite speakerAvatar);
    public void Hide();
}