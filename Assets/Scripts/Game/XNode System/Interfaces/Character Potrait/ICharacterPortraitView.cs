using System;
using UnityEngine;

public interface ICharacterPortraitView
{
    public event Action Complite;

    public void Show(ICharacterPortraitModel characterPortrait);

    public void Delete(ICharacterPortraitModel character);
}