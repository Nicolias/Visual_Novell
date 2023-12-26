using System;

namespace Shop
{
    public class ProductPresenter : IDisposable
    {
        private readonly ProductView _view;
        private readonly Product _model;

        private readonly SaveLoadServise _saveLoadServise;
        private readonly Shop _shop;

        private bool _isBought = false;
        private string _saveKey;

        public ProductPresenter(ProductView view, Product model, SaveLoadServise saveLoadServise, Shop shop)
        {
            _view = view;
            _model = model;

            _saveKey = model.Data.ID;

            if(saveLoadServise.HasSave(_saveKey))
                _isBought = saveLoadServise.Load<SaveData.BoolData>(_saveKey).Bool;

            if (model.Data.IsOnce && _isBought)
            {
                view.Delete();
                return;
            }

            view.BuyButtonClicked += OnBuyButtonClicked;            

            _shop = shop;
            _saveLoadServise = saveLoadServise;
        }

        public void Dispose()
        {
            _view.BuyButtonClicked -= OnBuyButtonClicked;
            _shop.ProductBought -= OnProudcrtBought;

        }

        private void OnBuyButtonClicked()
        {
            _shop.ProductBought += OnProudcrtBought;
            _shop.BuyProduct(_model);
        }

        private void OnProudcrtBought(Product product)
        {
            if (product != _model)
                throw new InvalidOperationException("Не правильный продукт");

            _isBought = true;
            _saveLoadServise.Save(_saveKey, new SaveData.BoolData() { Bool = _isBought });
            _shop.ProductBought -= OnProudcrtBought;

            if (product.Data.IsOnce)
            {
                Dispose();
                _view.Delete();
            }
        }
    }
}