using UnityEngine;

[CreateAssetMenu(fileName = "Product", menuName = "Shop")]
public class ProductSO : ScriptableObject
{
    [field: SerializeField] public bool IsOnce { get; private set; }
}