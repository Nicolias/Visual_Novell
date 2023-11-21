using UnityEngine;

public abstract class ItemForCollection : AbstractItem
{    
    [field: SerializeField] public Sprite ItemSpriteInScene { get; private set; }
    [field: SerializeField] public ItemForCollectionView Prefab { get; private set; }
    [field: SerializeField] public Vector2 ItemAfterInstantiatePosition { get; private set; }
    [field: SerializeField] public bool IsInteractable { get; private set; }

    public void Initialize(ItemForCollectionView itemView)
    {
        Prefab = itemView;
    }

    public abstract void Acсept(ItemCollector itemCollectorVisitor);
}
