using System;
using System.Collections.Generic;

public class SpawnItemCommand : ICommand
{
    private CollectionPanel _collectionPanel;
    private Location _location;
    private Item _item;

    public event Action Complete;

    public SpawnItemCommand(CollectionPanel collectionPanel, SpawnItemModel model)
    {
        _collectionPanel = collectionPanel;
        _item = model.Item;
        _location = model.Location;
    }

    public void Execute()
    {
        _location.Add(_item);
        _collectionPanel.ShowItems(_location.ItemsView);
        Complete?.Invoke();
    }
}