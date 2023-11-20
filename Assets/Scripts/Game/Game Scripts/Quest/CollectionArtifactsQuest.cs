using System;
using System.Collections.Generic;
using System.Linq;

public class CollectionArtifactsQuest : CollectionQuest<Artifact>
{
    private List<ItemForCollection> _artifacts;

    public CollectionArtifactsQuest(IEnumerable<Artifact> itemsForCollection, CollectionPanel collectionPanel) 
        : base( collectionPanel, itemsForCollection) 
    {
        _artifacts = ItemsForCollection.ToList();
    }

    public override event Action<Artifact> ItemCollected;

    protected override void OnItemSelected(ItemForCollectionView selectdItemView, ItemForCollection selectedItemData)
    {
        if (TryDeleteItem(selectdItemView, selectedItemData))
        {
            if (_artifacts.Contains(selectedItemData))
                ItemCollected?.Invoke((Artifact)selectedItemData);
        }
        else
        {
            throw new InvalidOperationException();
        }
    }
}