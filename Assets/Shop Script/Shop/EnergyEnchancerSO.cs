using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "EnergyEnchancer", menuName = "Shop/EnergyEnchancer")]
    public class EnergyEnchancerSO : ProductSO
    {
        [field: SerializeField] public int EnchanceValue { get; private set; }

        public override void Accept(ProductVisiter productVisiter)
        {
            productVisiter.Visit(this);
        }
    }
}