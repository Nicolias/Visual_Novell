using UnityEngine;

[CreateAssetMenu(menuName = "Item/Item")]
public class Item : ItemForCollection
{
    public override void Acсept(IItemCollector itemCollectorVisitor)
    {
        throw new System.NotImplementedException();
    }
}
