using UnityEngine;


public class Item : ScriptableObject
{
    [field: SerializeField] public ItemForCollectionView Prefab { get; private set; }
    [field: SerializeField] public Vector2 ItemAfterInstantiatePosition { get; private set; }
}

public abstract class ItemForCollection : Item
{
    [field: SerializeField] public Sprite ItemSpriteInInventory { get; private set; }
    [field: SerializeField] public Sprite ItemSpriteInScene { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Discription { get; private set; }

    public abstract void Acсept(IItemCollector itemCollectorVisitor);
}
