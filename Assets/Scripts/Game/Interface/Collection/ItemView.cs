using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _itemImage;

    private Item _itemData;

    public void Initialize(Item itemData)
    {
        _itemData = itemData;

        _itemImage.sprite = itemData.ItemSpriteInScene;
    }
}