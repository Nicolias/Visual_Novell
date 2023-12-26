using UnityEngine;

namespace Shop
{
    public abstract class ProductSO : ScriptableObject
    {
        [field: SerializeField] public Sprite Image { get; private set; }
        [field: SerializeField] public bool IsOnce { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string ID { get; private set; }

        public abstract void Accept(ProductVisiter productVisiter);
    }
}