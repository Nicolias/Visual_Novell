using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "MoneyAccruer", menuName = "Shop/MoneyAccruer")]
    public class MoneyAccruerSO : ProductSO
    {
        [field: SerializeField] public int Value { get; private set; }

        public override void Accept(ProductVisiter productVisiter)
        {
            productVisiter.Visit(this);
        }
    }
}