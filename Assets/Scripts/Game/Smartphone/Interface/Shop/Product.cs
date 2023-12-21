using System;

public class Product
{
    private readonly ProductSO _data;

    public Product(ProductSO data)
    {
        _data = data;
    }

    public ProductSO Data => _data;

    public event Action<ProductSO> Bought;

    public void BuyProduct()
    {
        Bought?.Invoke(_data);
    }
}

public class ProductPresenter : IDisposable
{
    private readonly ProductView _view;
    private readonly Product _model;

    public ProductPresenter(ProductView view, Product model)
    {
        _view = view;
        _model = model;

        _view.BuyButtonClicked += OnBuyButtonClicked;
        _model.Bought += OnProductBought;
    }

    public void Dispose()
    {
        _view.BuyButtonClicked -= OnBuyButtonClicked;
        _model.Bought -= OnProductBought;
    }

    private void OnProductBought(ProductSO productData)
    {
        if (productData.IsOnce)
            _view.Delete();
    }

    private void OnBuyButtonClicked()
    {
        _model.BuyProduct();
    }
}