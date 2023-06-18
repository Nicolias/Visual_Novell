using UnityEngine;

[CreateAssetMenu(menuName = "Category/ImageAndDiscription")]
public class DiscriptionWithImageData : DUXCategoryData
{
    [field: SerializeField] public Sprite Sprite { get; private set; }

    public override void Accept(IDUXVisitor visitor)
    {
        visitor.Visit(this);
    }
}