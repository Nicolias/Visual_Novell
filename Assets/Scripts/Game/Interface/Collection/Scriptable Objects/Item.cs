using UnityEngine;

[CreateAssetMenu(menuName = "Item/Item")]
public class Item : ItemForCollection
{
    public override void Acсept(ItemCollector itemCollectorVisitor)
    {
        throw new System.NotImplementedException();
    }
}
