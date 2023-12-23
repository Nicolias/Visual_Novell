using System.Collections.Generic;
using UnityEngine;
using VisualNovell;

public class ProductFactory : MonoBehaviour
{
    [SerializeField] private ProductView _viewTemplate;
    [SerializeField] private Transform _container;

    public List<ProductView> CreatProducts(IReadOnlyList<ProductSO> products)
    {
        List<ProductView> productViews = new List<ProductView>();

        foreach (var productData in products)
        {
            ProductView newProductView = Instantiate(_viewTemplate, _container);
            Product newProductModel = new Product(productData);
            newProductView.Initialize(newProductModel);
        }

        return productViews;
    }
}