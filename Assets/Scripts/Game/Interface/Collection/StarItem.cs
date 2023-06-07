using UnityEngine;

[CreateAssetMenu(fileName = "Star", menuName = "Item/Star")]
public class StarItem : ItemForCollection
{
    public override void Acсept(IItemCollector itemCollectorVisitor)
    {
        itemCollectorVisitor.Visit(this);
    }
}