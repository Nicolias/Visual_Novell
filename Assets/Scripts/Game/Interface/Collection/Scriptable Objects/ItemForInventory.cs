using System;
using UnityEngine;

public abstract class ItemForInventory : AbstractItem, IItemForInventory
{
    [field: SerializeField] public Sprite ItemSpriteInInventory { get; private set; }
    [field: SerializeField] public string Discription { get; private set; }

    [field: SerializeField] public bool HaveEffect { get; private set; }

    [field: SerializeField] public int ID { get; private set; }
}

public interface IItemForInventory
{
    public Sprite ItemSpriteInInventory { get; }
    public string Name { get; }
    public string Discription { get; }

    public bool HaveEffect { get; }
    public int ID { get; }
}

public interface IUseableItemForInventory : IItemForInventory
{
    public int UseCount { get; }
    public abstract void Accept(UsableInventoryCell inventoryCell);
}