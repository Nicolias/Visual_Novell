using System;

public class Product
{
    private readonly ProductSO _data;

    public Product(ProductSO data)
    {
        _data = data;
    }

    public event Action<bool> Bought;

    public void BuyProduct()
    {
        Bought?.Invoke(_data.IsOnce);
    }
}

public class ProductPresenter : IDisposable
{
    private readonly ShopItem _view;
    private readonly Product _model;

    public ProductPresenter(ShopItem view, Product model)
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

    private void OnProductBought(bool isOnce)
    {
        if (isOnce)
            _view.Delete();
    }

    private void OnBuyButtonClicked()
    {
        _model.BuyProduct();
    }
}