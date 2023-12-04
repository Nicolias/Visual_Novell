using Characters;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public interface ILocation : IDataForCell, IDisposable
{
    public LocationSO Data { get; }
    
    public bool IsAvailable { get; }

    public Sprite DefultSprite { get; }

    public int ID { get; }
    public IEnumerable<ItemForCollectionView> ItemsView { get; }

    public event Action<ILocation, Node> QuestStarted;

    public void Show();
    
    public void Add(ItemForCollection artifact);

    public void DeleteArtifacts();

    public void Set(CharacterSO character);

    public void Set(Node questOnLocation);

    public void RemoveQuest();
}
