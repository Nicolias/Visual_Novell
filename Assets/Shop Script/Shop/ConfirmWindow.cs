using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shop
{
    public class ConfirmWindow : MonoBehaviour
    {
        [SerializeField] private Button _completeButton;
        [SerializeField] private Button _cancelButton;

        [SerializeField] private TMP_Text _productName;
        [SerializeField] private Image _productImage;

        private ProductPresenter _presenter;

        public event UnityAction<ProductPresenter> BuyCompleted;

        private void OnEnable()
        {
            _completeButton.onClick.AddListener(OnComplete);
            _cancelButton.onClick.AddListener(OnCanceled);
        }

        private void OnDisable()
        {
            _completeButton.onClick.RemoveListener(OnComplete);
            _cancelButton.onClick.RemoveListener(OnCanceled);
        }

        public void Show(ProductPresenter presenter)
        {
            gameObject.SetActive(true);

            _presenter = presenter;
            _productName.text = presenter.Product.Data.Name;
            _productImage.sprite = presenter.Product.Data.Image;
        }

        private void OnComplete()
        {
            BuyCompleted.Invoke(_presenter);
            gameObject.SetActive(false);
        }

        private void OnCanceled()
        {
            gameObject.SetActive(false);
        }
    }
}