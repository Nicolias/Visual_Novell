using UnityEngine;
using UnityEngine.UI;

public interface ICharacterView
{
    public Image Image { get; }
    public void Initialize(CharacterType character, Meeting meeting, Location location);
    public GameObject GameObject { get; }
}