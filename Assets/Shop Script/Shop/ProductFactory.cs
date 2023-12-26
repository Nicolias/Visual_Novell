using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    public class ProductFactory : MonoBehaviour
    {
        [SerializeField] private ProductView _viewTemplate;
        [SerializeField] private Transform _container;

        private Shop _shop;
        private SaveLoadServise _saveLoadServise;

        private List<ProductView> _productViews;

        public void Initialize(Shop shop, SaveLoadServise saveLoadServise, IReadOnlyList<ProductSO> products)
        {
            _shop = shop;
            _saveLoadServise = saveLoadServise;

            CreatProducts(products);
        }

        private void CreatProducts(IReadOnlyList<ProductSO> products)
        {
            List<ProductView> productViews = new List<ProductView>();

            foreach (var productData in products)
            {
                ProductView newProductView = Instantiate(_viewTemplate, _container);
                Product newProductModel = new Product(productData);
                newProductView.Initialize(newProductModel, _saveLoadServise, _shop);
            }

            _productViews = productViews;
        }
    }
}