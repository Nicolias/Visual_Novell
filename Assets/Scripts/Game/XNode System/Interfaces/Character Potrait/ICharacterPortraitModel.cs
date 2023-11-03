using UnityEngine;

public interface ICharacterPortraitModel
{
    public CharacterType CharacterType { get; }
    public string Name { get; }
    public Sprite Sprite { get; }
    public CharacterPortraitPosition PositionType { get; }

    public Location Location { get; }

    public Vector2 PositionOffset { get; }
    public Vector3 ScaleOffset { get; }
}