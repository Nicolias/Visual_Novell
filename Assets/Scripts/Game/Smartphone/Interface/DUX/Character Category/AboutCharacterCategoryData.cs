using UnityEngine;

[CreateAssetMenu(menuName = "Category/Character/Information")]
public class AboutCharacterCategoryData : CharacterCategoryData
{
    public override void Accept(IDUXVisitor visitor)
    {
        visitor.Visit(this);
    }
}
