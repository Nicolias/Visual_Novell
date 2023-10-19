using System;
using UnityEngine;

public interface ISpeechView
{
    public event Action Clicked;

    public ShowTextStatus ShowStatus { get; }
    public void ShowSmooth(ISpeechModel speechModel);
    public void Show(ISpeechModel speechModel);
    public void Hide();
}