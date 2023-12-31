using UnityEngine;

namespace Shop
{
    public class ProductVisiter : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private Battery _battery;
        [SerializeField] private Inventory _inventory;

        public ProductVisiter(Wallet wallet, Battery battery, Inventory inventory)
        {
            _wallet = wallet;
            _battery = battery;
            _inventory = inventory;
        }

        public void Visit(EnergyAccruerSO energyAccuer)
        {
            _inventory.AddItemToInventory(energyAccuer.EnergyBooster);
        }

        public void Visit(MoneyAccruerSO moneyAccruer)
        {
            _wallet.AccureWithOutPanel(moneyAccruer.Value);
        }

        public void Visit(EnergyEnchancerSO energyEnchancer)
        {
            _battery.Enchance(energyEnchancer.EnchanceValue);
        }

        public void Visit(ProductSO product)
        {
            product.Accept(this);
        }
    }
}