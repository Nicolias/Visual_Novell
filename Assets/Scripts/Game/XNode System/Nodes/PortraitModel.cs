using UnityEngine;
using UnityEngine.UI;

public class PortraitModel : XnodeModel, ICharacterPortraitModel
{
    [field: SerializeField] public CharacterType CharacterType { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public CharacterPortraitPosition PositionType { get; private set; }

    public Vector2 PositionOffset => new Vector2();

    public Vector3 ScaleOffset => new Vector3();

    public LocationSO Location => null;

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}