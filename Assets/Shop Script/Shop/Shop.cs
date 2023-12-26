using RuStore;
using RuStore.BillingClient;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Shop
{
    public class Shop : WindowInSmartphone
    {
        [Inject] private SaveLoadServise _saveLoadServise;

        [SerializeField] private Canvas _selfCanvas;
        [SerializeField] private Button _closeButton;

        [SerializeField] private Wallet _wallet;
        [SerializeField] private Battery _battery;
        [SerializeField] private Inventory _inventory;

        [SerializeField] public ProductFactory _productsFactory;
        [SerializeField] private List<ProductSO> _productsSO;

        private ProductVisiter _productVisiter;

        public event UnityAction<Product> ProductBought;

        private void Awake()
        {
            RuStoreBillingClient.Instance.Init();

            _productVisiter = new ProductVisiter(_wallet, _battery, _inventory);

            _productsFactory.Initialize(this, _saveLoadServise, _productsSO);
        }

        protected override void OnEnabled()
        {
            _closeButton.onClick.AddListener(Close);
        }


        protected override void OnDisabled()
        {
            _closeButton.onClick.RemoveListener(Close);
        }

        public void BuyProduct(Product product)
        {
            _productVisiter.Visit(product.Data);
            ProductBought?.Invoke(product);

            //RuStoreBillingClient.Instance.PurchaseProduct(
            //    productId: product.Data.ID,
            //    quantity: 1,
            //    developerPayload: "test payload",
            //    onFailure: (error) =>
            //    {
            //        OnError(error);
            //    },
            //    onSuccess: (result) =>
            //    {
            //    });
        }

        protected override void OnOpenButtonClicked()
        {
            _selfCanvas.enabled = true;
        }

        private void OnError(RuStoreError error)
        {
            Debug.LogErrorFormat("{0} : {1}", error.name, error.description);
        }

        private void Close()
        {
            _selfCanvas.enabled = false;
        }
    }

    public class ProductVisiter
    {
        private readonly Wallet _wallet;
        private readonly Battery _battery;
        private readonly Inventory _inventory;

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
            _wallet.Accure(moneyAccruer.Value);
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