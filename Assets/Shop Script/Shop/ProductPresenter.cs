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

        public Product Product => _model;

        public void Dispose()
        {
            _view.BuyButtonClicked -= OnBuyButtonClicked;
        }

        public void Buy()
        {
            _isBought = true;
            _saveLoadServise.Save(_saveKey, new SaveData.BoolData() { Bool = _isBought });

            if (_model.Data.IsOnce)
            {
                Dispose();
                _view.Delete();
            }
        }

        private void OnBuyButtonClicked()
        {
            _shop.BuyProduct(this);
        }
    }
}