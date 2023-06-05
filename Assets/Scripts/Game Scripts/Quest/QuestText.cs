using TMPro;
using UnityEngine;

public class QuestText : MonoBehaviour
{
    [SerializeField] private TMP_Text _questName;
    public ItemForCollection ItemForCollection { get; private set; }

    public void Initialize(ItemForCollection item)
    {
        ItemForCollection = item;
        _questName.text = item.Name;
    }

    public void Complete()
    {
        _questName.fontStyle = FontStyles.Strikethrough | FontStyles.Bold;
    }
}