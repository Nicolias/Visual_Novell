using UnityEngine;

public abstract class ItemForCollection : ScriptableObject
{
    [field: SerializeField] public Sprite ItemSpriteInInventory { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Discription { get; private set; }
    [field: SerializeField] public Sprite ItemSpriteInScene { get; private set; }
    [field: SerializeField] public ItemForCollectionView Prefab { get; private set; }
    [field: SerializeField] public Vector2 ItemAfterInstantiatePosition { get; private set; }
    [field: SerializeField] public bool IsInteractable { get; private set; }

    public abstract void Acсept(IItemCollector itemCollectorVisitor);
}
