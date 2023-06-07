using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectionQuestView : MonoBehaviour
{
    [SerializeField] private Transform _questsTextContainer;
    [SerializeField] private QuestText _textTemplate;

    private CollectionQuest _currentQust;
    private List<QuestText> _itemsForCollection;

    public void Initialize(CollectionQuest currentQuest)
    {
        gameObject.SetActive(true);

        _currentQust = currentQuest;
        _itemsForCollection = new();

        foreach (ItemForCollection item in currentQuest.ItemForCollections)
        {
            var itemQuest = Instantiate(_textTemplate, _questsTextContainer);
            itemQuest.Initialize(item);
            _itemsForCollection.Add(itemQuest);
        }

        _currentQust.OnItemCollected += OnItemCollect;
    }

    private void OnItemCollect(ItemForCollection item)
    {
        var itemForCollectionText = _itemsForCollection.Find(x => x.ItemForCollection == item);
        itemForCollectionText.Complete();
        _itemsForCollection.Remove(itemForCollectionText);

        if (_itemsForCollection.Count == 0)
        {
            _currentQust.OnItemCollected -= OnItemCollect;

            _itemsForCollection = null;
            _currentQust = null;
            gameObject.SetActive(false);
        }

    }
}
