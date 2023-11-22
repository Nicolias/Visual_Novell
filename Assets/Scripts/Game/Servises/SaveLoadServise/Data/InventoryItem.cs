using System;
using UnityEngine;

namespace SaveData
{
    [Serializable]
    public class InventoryItems : IItemForInventory
    {
        public InventoryItems(IItemForInventory item)
        {
            ItemSpriteInInventory = item.ItemSpriteInInventory;
            Name = item.Name;
            Discription = item.Discription;
            HaveEffect = item.HaveEffect;
        }

        public Sprite ItemSpriteInInventory { get; set; }
        public string Name { get; set; }
        public string Discription { get;  set; }
        public bool HaveEffect { get; set; }
    }
    
    [Serializable]
    public class UsableInventoryItem : InventoryItems, IUseableItemForInventory
    {
        public UsableInventoryItem(IUseableItemForInventory item, int useCount) : base(item)
        {
            UseCount = useCount;
        }

        public int UseCount { get; set; }

        public void Accept(UsableInventoryCell inventoryCell)
        {
            throw new NotImplementedException();
        }
    }
}