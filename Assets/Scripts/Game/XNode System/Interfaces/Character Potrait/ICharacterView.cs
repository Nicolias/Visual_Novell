using UnityEngine;
using UnityEngine.UI;

public interface ICharacterView
{
    public Image Image { get; }
    public void Initialize(ICharacterPortraitModel characterData, Meeting meeting);
    public GameObject GameObject { get; }
}