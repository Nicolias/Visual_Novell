using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name;

    [SerializeField] private Button _buyButton;

    private ProductPresenter _presenter;

    public event UnityAction BuyButtonClicked;

    public void Initialize(Product product)
    {
        _presenter = new ProductPresenter(this, product);
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(BuyButtonClicked);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(BuyButtonClicked);
    }

    public void Delete()
    {
        _presenter.Dispose();

        Destroy(gameObject);
    }
}