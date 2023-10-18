using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectionQuestView : MonoBehaviour
{
    [SerializeField] private Transform _questsTextContainer;
    [SerializeField] private QuestText _textTemplate;

    private CollectionQuest _currentQuest;
    private List<QuestText> _itemsNameForCollection = new List<QuestText>();

    public void Initialize(CollectionQuest currentQuest)
    {
        gameObject.SetActive(true);

        _currentQuest = currentQuest;

        foreach (var item in currentQuest.ItemsForCollection)
        {
            var itemQuest = Instantiate(_textTemplate, _questsTextContainer);
            itemQuest.Initialize(item);
            _itemsNameForCollection.Add(itemQuest);
        }

        _currentQuest.ItemCollected += OnItemCollect;
        _currentQuest.QuestCompleted += OnQuestComplete;
    }

    private void OnQuestComplete()
    {
        _currentQuest.ItemCollected -= OnItemCollect;
        _currentQuest.QuestCompleted -= OnQuestComplete;

        _itemsNameForCollection = null;
        _currentQuest = null;
        gameObject.SetActive(false);
    }

    private void OnItemCollect(ItemForCollection item)
    {
        var itemForCollectionText = _itemsNameForCollection.Find(itemName => itemName.ItemForCollection == item);
        itemForCollectionText.Complete();
        _itemsNameForCollection.Remove(itemForCollectionText);
    }
}