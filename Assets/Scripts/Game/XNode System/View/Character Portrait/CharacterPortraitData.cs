using UnityEngine;
using UnityEngine.UI;

public class CharacterPortraitData
{
    public string Name { get; private set; }
    public Image Image { get; private set; }
    public CharacterPortraitPosition Position { get; private set; }
    public CharacterType CharacterType { get; private set; }
    public Vector2 PositionOffset { get; private set; }
    public Vector3 ScalseOffset { get; private set; }

    public CharacterPortraitData(ICharacterPortraitModel characterPortrait, Image characterImage)
    {
        Name = characterPortrait.Name;
        Image = characterImage;
        Position = characterPortrait.PositionType;
        CharacterType = characterPortrait.CharacterType;
        PositionOffset = characterPortrait.PositionOffset;
        ScalseOffset = characterPortrait.ScaleOffset;
    }

    public void SetPosition(CharacterPortraitPosition newPosition)
    {
        Position = newPosition;
    }

    public void ChangeImageSpriteOn(Sprite sprite)
    {
        Image.sprite = sprite;
    }
}
