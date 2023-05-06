using UnityEngine;

public interface ICharacterPortraitModel
{
    public string Name { get; }
    public Sprite Sprite { get; }
    public CharacterPortraitPosition Position { get; }
}