using System;

public class SpawnItemCommand : ICommand
{
    public event Action Completed;

    private Map _map;
    private CollectionPanel _collectionPanel;
    private Item _item;
    private LocationType _locationType;

    public SpawnItemCommand(Map map, CollectionPanel collectionPanel, SpawnItemModel model)
    {
        _map = map;
        _collectionPanel = collectionPanel;
        _item = model.Item;
        _locationType = model.Location;
    }

    public void Execute()
    {
        _map.Add(_item, _locationType);
        _collectionPanel.ShowItems(new() { _item});
        Completed?.Invoke();
    }
}