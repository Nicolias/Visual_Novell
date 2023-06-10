using UnityEngine;

[CreateAssetMenu(menuName = "Category/Imgae")]
public class CharacterImageCategoryData : DUXCategoryData
{
    public override void Accept(IDUXVisitor visitor)
    {
        visitor.Visit(this);
    }
}