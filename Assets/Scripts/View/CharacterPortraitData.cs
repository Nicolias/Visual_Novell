using UnityEngine.UI;

public class CharacterPortraitData
{
    public string Name { get; private set; }
    public Image Image { get; private set; }
    public CharacterPortraitPosition Position { get; private set; }

    public CharacterPortraitData(string name, Image image, CharacterPortraitPosition position)
    {
        Name = name;
        Image = image;
        Position = position;
    }

    public void ChangePosition(CharacterPortraitPosition newPosition)
    {
        Position = newPosition;
    }
}
