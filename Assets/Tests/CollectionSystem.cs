using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using FluentAssertions;
using System.Linq;

public class CollectionSystem
{
    [Test]
    public void WhenQuesStarting_AndItemSelected_ThenQuestComplete()
    {
        // Arrange.
        CollectionPanel collectionPanel = new GameObject().AddComponent<CollectionPanel>();
        IInventory inventory = Substitute.For<IInventory>();

        ItemForCollection itemData = ScriptableObject.CreateInstance<KeyItem>();
        itemData.Initialize(new GameObject().AddComponent<ItemForCollectionView>());

        CollectionQuest collectionQuest = new CollectionQuest(new() { itemData }, collectionPanel, inventory);
        ItemForCollectionView itemForQuestCollection = collectionQuest.ItemsForCollection.ToList()[0];

        // Act.
        itemForQuestCollection.SelectItem();

        // Assert.
        collectionQuest.IsQuestComplete.Should().Be(true);
    }
}
