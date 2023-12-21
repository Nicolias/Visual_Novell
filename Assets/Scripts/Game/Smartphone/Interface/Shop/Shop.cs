using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ProductSO> _productsData;
    [SerializeField] private ProductFactory _productsFactory;

    private List<ProductView> _productViews;

    private void Awake()
    {
        _productViews = _productsFactory.CreatProducts(_productsData);
    }
}