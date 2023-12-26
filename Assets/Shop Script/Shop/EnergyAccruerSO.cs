using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "EnergyAccruer", menuName = "Shop/EnergyAccruer")]
    public class EnergyAccruerSO : ProductSO
    {
        [field: SerializeField] public EnergyBooster EnergyBooster { get; private set; }

        public override void Accept(ProductVisiter productVisiter)
        {
            productVisiter.Visit(this);
        }
    }
}