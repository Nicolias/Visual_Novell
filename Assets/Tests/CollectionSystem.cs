using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using FluentAssertions;
using System.Linq;

public class CollectionSystem
{
    private CollectionPanel _collectionPanel;
    private IInventory _inventory;

    [SetUp]
    public void SetUp()
    {
        _collectionPanel = new GameObject().AddComponent<CollectionPanel>();
        _inventory = Substitute.For<IInventory>();
    }

    [Test]
    public void WhenShowingItems_AndItemSelected_ThenCollectionPanelKnownIt()
    {
        // Arrange.
        ItemForCollectionView itemView = _collectionPanel.CreateItemsView(CreateItemsData(1))[0];
        bool isItemSelected = false;

        _collectionPanel.ItemSelected += (itemView, itemData) => isItemSelected = true;
        _collectionPanel.ShowItems(new() { itemView });

        // Act.
        itemView.SelecteItem();

        // Assert.
        isItemSelected.Should().BeTrue();   
    }


    [Test]
    public void WhenItemHiding_AndItemSelected_ThenColectionPanelDontKnownIt()
    {
        // Arrange.
        ItemForCollectionView hidedItemView = _collectionPanel.CreateItemsView(CreateItemsData(1))[0];
        ItemForCollectionView showedItemView = _collectionPanel.CreateItemsView(CreateItemsData(1))[0];
        bool isItemSelected = false;

        _collectionPanel.ItemSelected += (itemView, itemData) => isItemSelected = true;
        _collectionPanel.ShowItems(new() { hidedItemView });
        _collectionPanel.ShowItems(new() { showedItemView });

        // Act.
        hidedItemView.SelecteItem();

        // Assert.
        isItemSelected.Should().BeFalse();
    }

    [Test]
    public void WhenStartingQuest_AndItemSelected_ThenQuestComplete()
    {
        // Arrange.
        bool isQuestComplete = false;

        List<ItemForCollection> itemsForCollection = CreateItemsData(1);
        ItemForCollectionView itemViewForQuestCollection = _collectionPanel.CreateItemsView(itemsForCollection)[0];

        CollectionQuest collectionQuest = new CollectionQuest(itemsForCollection, _collectionPanel, _inventory);
        _collectionPanel.ShowItems(new() { itemViewForQuestCollection });
        collectionQuest.QuestCompleted += () => isQuestComplete = true;

        // Act.
        itemViewForQuestCollection.SelecteItem();

        // Assert.
        isQuestComplete.Should().BeTrue();
    }

    private List<ItemForCollection> CreateItemsData(int itemsCount)
    {
        List<ItemForCollection> items = new List<ItemForCollection>();

        for (int i = 0; i < itemsCount; i++)
        {
            ItemForCollection itemData = ScriptableObject.CreateInstance<KeyItem>();
            itemData.Initialize(new GameObject().AddComponent<ItemForCollectionView>());
            items.Add(itemData);
        }

        return items;
    }
}