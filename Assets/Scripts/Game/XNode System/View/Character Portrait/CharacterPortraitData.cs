using UnityEngine;
using UnityEngine.UI;

public class CharacterPortraitData
{
    public string Name { get; private set; }
    public Image Image { get; private set; }
    public CharacterPortraitPosition Position { get; private set; }
    public CharacterType CharacterType { get; private set; }

    public CharacterPortraitData(ICharacterPortraitModel characterPortrait, Image characterImage)
    {
        Name = characterPortrait.Name;
        Image = characterImage;
        Position = characterPortrait.Position;
        CharacterType = characterPortrait.CharacterType;
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
