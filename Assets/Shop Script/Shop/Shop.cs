using RuStore;
using RuStore.BillingClient;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    

    private List<ShopItemView> _purchaseViews;
    private Dictionary<string, Product> _products = new Dictionary<string, Product>();

    public void BuyProduct(string productId)
    {
        RuStoreBillingClient.Instance.PurchaseProduct(
            productId: productId,
            quantity: 1,
            developerPayload: "test payload",
            onFailure: (error) => 
            {
                OnError(error);
            },
            onSuccess: (result) => 
            {
                LoadPurchases();
            });
    }

    private void LoadProducts()
    {
        RuStoreBillingClient.Instance.GetProducts(
            productIds: _productIds,
            onFailure: (error) => 
            {
                OnError(error);
            },
            onSuccess: (result) => 
            {
                _products.Clear();
                result.ForEach(p => _products.Add(p.productId, p));

                UpdateProductsData(result);
                LoadPurchases();
            });
    }

    private void UpdatePurchaseData(List<Purchase> purchases)
    {
        foreach (var shopItem in _purchaseViews)
        {
            shopItem.Data = null;
            shopItem.gameObject.SetActive(false);
        }

        var viewIndex = 0;
        foreach (var purchase in purchases)
        {
            if (purchase.purchaseState == Purchase.PurchaseState.PAID)
            {
                _purchaseViews[viewIndex].gameObject.SetActive(true);
                _purchaseViews[viewIndex].Data = purchase;
                if (++viewIndex >= _purchaseViews.Length)
                {
                    break;
                }
            }
        }
    }

    private void OnError(RuStoreError error)
    {
        Debug.LogErrorFormat("{0} : {1}", error.name, error.description);
    }
}
