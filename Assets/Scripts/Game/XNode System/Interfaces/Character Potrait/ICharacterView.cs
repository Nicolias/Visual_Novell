using UnityEngine;
using UnityEngine.UI;

public interface ICharacterView
{
    public Image Image { get; }
    public void Initialize(ICharacterPortraitModel characterData, Meeting meeting);

    public void SetInteractable(bool isInteractable);

    public GameObject GameObject { get; }
}