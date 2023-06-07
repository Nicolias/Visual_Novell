using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventoryCell : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemNameText;

    private Button _selfButton;
    private ItemForCollection _data;

    private TMP_Text _itemDiscription;
    private Image _itamImage;

    private void Awake()
    {
        _selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(DisplayItemInformation);
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
    }

    public void Initialize(ItemForCollection itemData, Image itemImage, TMP_Text itemDiscription)
    {
        _data = itemData;
        _itemNameText.text = itemData.Name;

        _itamImage = itemImage;
        _itemDiscription = itemDiscription;
    }

    private void DisplayItemInformation()
    {
        _itamImage.sprite = _data.ItemSpriteInInventory;
        _itemDiscription.text = _data.Discription;

        _itamImage.color = new(1, 1, 1, 1);
    }
}