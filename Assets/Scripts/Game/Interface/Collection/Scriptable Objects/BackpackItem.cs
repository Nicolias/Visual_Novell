using UnityEngine;

[CreateAssetMenu(fileName = "Backpack", menuName = "Item/Backpack")]
public class BackpackItem : ItemForCollection, IItemForInventory
{
    [field: SerializeField] public Sprite ItemSpriteInInventory { get; private set; }
    [field: SerializeField] public string Discription { get; private set; }

    [field: SerializeField] public bool HaveEffect { get; private set; }
    [field: SerializeField] public int ID { get; private set; }

    public void Accept(InventoryCellView inventoryCell)
    {
        throw new System.NotImplementedException();
    }

    public override void Acсept(ItemCollector itemCollectorVisitor)
    {
        itemCollectorVisitor.Visit(this);
    }
}
