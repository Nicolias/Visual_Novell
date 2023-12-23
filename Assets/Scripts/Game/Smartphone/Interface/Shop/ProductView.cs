using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VisualNovell;

namespace VisualNovell
{

    public class ProductView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _name;

        [SerializeField] private Button _buyButton;

        private ProductPresenter _presenter;

        public event UnityAction BuyButtonClicked;

        public void Initialize(Product product)
        {
            _presenter = new ProductPresenter(this, product);
            _image.sprite = product.Data.Image;
            _name.text = product.Data.Name;
        }

        private void OnEnable()
        {
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
        }

        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
        }

        public void Delete()
        {
            _presenter.Dispose();

            Destroy(gameObject);
        }

        private void OnBuyButtonClicked()
        {
            BuyButtonClicked?.Invoke();
        }
    }
}