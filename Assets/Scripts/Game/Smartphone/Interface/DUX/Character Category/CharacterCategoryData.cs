using UnityEngine;

[CreateAssetMenu(menuName = "Category/Character")]
public class CharacterCategoryData : DUXCategoryData
{
    [field: SerializeField] public Sprite CharacterSprite { get; private set; }

    public override void Accept(IDUXVisitor visitor)
    {
        visitor.Visit(this);
    }
}
