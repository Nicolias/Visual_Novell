using UnityEngine;

[CreateAssetMenu(fileName = "Key", menuName = "Item/Key")]
public class KeyItem : ItemForCollection
{
    public override void Acсept(IItemCollector itemCollectorVisitor)
    {
        itemCollectorVisitor.Visit(this);
    }
}
