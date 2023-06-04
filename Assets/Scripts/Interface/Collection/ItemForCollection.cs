using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Item")]
public class ItemForCollection : ScriptableObject
{
    [field: SerializeField] public Sprite ItemSpriteInInventory { get; private set; }
    [field: SerializeField] public Sprite ItemSpriteInScene { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public ItemForCollectionView Prefab { get; private set; }
    [field: SerializeField] public Vector2 ItemAfterInstantiatePosition { get; private set; }
}