using UnityEngine;

[CreateAssetMenu(menuName = "Category/Melody")]
public class MelodyCategoryData : DUXCategoryData
{
    [field: SerializeField] public AudioClip AudioClip { get; private set; }

    public override void Accept(IDUXVisitor visitor)
    {
        visitor.Visit(this);
    }
}