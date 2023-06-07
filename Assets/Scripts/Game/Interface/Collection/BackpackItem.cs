using UnityEngine;

[CreateAssetMenu(fileName = "Backpack", menuName = "Item/Backpack")]
public class BackpackItem : ItemForCollection
{
    public override void Acсept(IItemCollector itemCollectorVisitor)
    {
        itemCollectorVisitor.Visit(this);
    }
}
