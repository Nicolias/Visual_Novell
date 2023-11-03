using UnityEngine;

public class CharacterOnLocationData : ICharacterPortraitModel
{
    public CharacterOnLocationData(CharacterType characterType, string name, Sprite sprite, Location location,
        CharacterPortraitPosition positionType, Vector2 positionOffset, Vector3 scaleOffset)
    {
        CharacterType = characterType;
        Name = name;
        Sprite = sprite;
        Location = location;
        PositionType = positionType;
        PositionOffset = positionOffset;
        ScaleOffset = scaleOffset;
    }

    public CharacterType CharacterType { get; private set; }

    public string Name { get; private set; }

    public Sprite Sprite { get; private set; }

    public CharacterPortraitPosition PositionType { get; private set; }

    public Vector2 PositionOffset { get; private set; }

    public Vector3 ScaleOffset { get; private set; }

    public Location Location { get; private set; }
}