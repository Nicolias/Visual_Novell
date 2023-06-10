using UnityEngine;

[CreateAssetMenu(menuName = "Category/Imgae/Variation")]
public class CharacterImageVariationCategoryData : DUXCategoryData
{
    [field: SerializeField] public Sprite CharacterSprite { get; private set; }

    public override void Accept(IDUXVisitor visitor)
    {
        visitor.Visit(this);
    }
}
