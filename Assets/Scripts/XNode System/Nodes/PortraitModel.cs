using UnityEngine;

public class PortraitModel : XnodeModel, ICharacterPortraitModel
{
    [field: SerializeField] public CharacterType CharacterType { get; private set; }

    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public Sprite Sprite { get; private set; }

    [field: SerializeField]
    public CharacterPortraitPosition Position { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Set(CharacterType ch, string st)
    {
        if(Name == st)
            CharacterType = ch;
    }
}