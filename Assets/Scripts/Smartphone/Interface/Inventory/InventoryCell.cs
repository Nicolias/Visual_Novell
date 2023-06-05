using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventoryCell : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemNameText;

    private Button _selfButton;
    private ItemForCollection _data;

    private void Awake()
    {
        _selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
    }

    public void Initialize(ItemForCollection itemData)
    {
        _data = itemData;
        _itemNameText.text = itemData.Name;
    }
}