using System;
using UnityEngine;

public interface ICharacterPortraitView
{
    public event Action OnComplite;

    public void Show(ICharacterPortraitModel characterPortrait);
}