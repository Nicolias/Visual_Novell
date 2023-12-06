using UnityEngine;

[CreateAssetMenu(menuName = "Category/Character")]
public class CharacterCategoryData : DUXCategoryData
{
    [field: SerializeField] public Sprite CharacterSprite { get; private set; }
    [field: SerializeField] public CharacterType CharacterType { get; private set; }

    public sealed override void Accept(IDUXVisitor visitor)
    {
        visitor.Visit(this);
    }
}
