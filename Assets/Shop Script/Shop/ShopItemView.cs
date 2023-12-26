using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name;

    [SerializeField] private Button _buyButton;

    public event UnityAction BuyButtonClicked; 

    public void SetItem(object item)
    {

    }
}