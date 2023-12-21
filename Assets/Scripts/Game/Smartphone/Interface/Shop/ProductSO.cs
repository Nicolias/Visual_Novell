using UnityEngine;

[CreateAssetMenu(fileName = "Product", menuName = "Shop/Product")]
public class ProductSO : ScriptableObject
{
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public bool IsOnce { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
}