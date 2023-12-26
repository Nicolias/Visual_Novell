using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shop
{
    public class ProductView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _name;

        [SerializeField] private Button _buyButton;

        public event UnityAction BuyButtonClicked;

        public void Initialize(Product product, SaveLoadServise saveLoadServise, Shop shop)
        {
            _image.sprite = product.Data.Image;
            _name.text = product.Data.Name;

            new ProductPresenter(this, product, saveLoadServise, shop);
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
            gameObject.SetActive(false);
        }

        private void OnBuyButtonClicked()
        {
            BuyButtonClicked?.Invoke();
        }        
    }
}