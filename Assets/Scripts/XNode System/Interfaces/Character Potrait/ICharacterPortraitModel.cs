using UnityEngine;

public interface ICharacterPortraitModel
{
    public CharacterType CharacterType { get; }
    public string Name { get; }
    public Sprite Sprite { get; }
    public CharacterPortraitPosition Position { get; }
}