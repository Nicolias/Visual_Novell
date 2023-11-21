using UnityEngine;

[CreateAssetMenu(fileName = "Key", menuName = "Item/Key")]
public class KeyItem : ItemForCollection, IItemForInventory
{
    [field: SerializeField] public Sprite ItemSpriteInInventory { get; private set; }
    [field: SerializeField] public string Discription { get; private set; }

    [field: SerializeField] public bool HaveEffect { get; private set; }

    public void Accept(InventoryCellView inventoryCell)
    {
        throw new System.NotImplementedException();
    }

    public override void Acсept(ItemCollector itemCollectorVisitor)
    {
        itemCollectorVisitor.Visit(this);
    }
}
