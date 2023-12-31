using RuStore;
using RuStore.BillingClient;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Shop
{
    [RequireComponent(typeof(ProductVisiter))]
    [RequireComponent(typeof(ProductFactory))]
    public class Shop : WindowInSmartphone
    {
        [Inject] private SaveLoadServise _saveLoadServise;

        [SerializeField] private Canvas _selfCanvas;
        [SerializeField] private Button _closeButton;

        [SerializeField] private List<ProductSO> _productsSO;
        [SerializeField] private ConfirmWindow _confirmWindow;

        public ProductFactory _productsFactory;
        private ProductVisiter _productVisiter;

        private void Awake()
        {
            RuStoreBillingClient.Instance.Init();

            _productVisiter = GetComponent<ProductVisiter>();
            _productsFactory.Initialize(this, _saveLoadServise, _productsSO);
        }

        protected override void OnEnabled()
        {
            _confirmWindow.BuyCompleted += OnBuyCompleted;
            _closeButton.onClick.AddListener(Close);
        }

        protected override void OnDisabled()
        {
            _confirmWindow.BuyCompleted -= OnBuyCompleted;
            _closeButton.onClick.RemoveListener(Close);
        }

        public void BuyProduct(ProductPresenter productPresenter)
        {
            _confirmWindow.Show(productPresenter);
        }

        protected override void OnOpenButtonClicked()
        {
            _selfCanvas.enabled = true;
        }

        private void OnBuyCompleted(ProductPresenter presenter)
        {
            _productVisiter.Visit(presenter.Product.Data);
            presenter.Buy();

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

        private void OnError(RuStoreError error)
        {
            Debug.LogErrorFormat("{0} : {1}", error.name, error.description);
        }

        private void Close()
        {
            _selfCanvas.enabled = false;
        }
    }
}