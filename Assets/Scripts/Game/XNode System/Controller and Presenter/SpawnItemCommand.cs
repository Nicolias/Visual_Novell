using System;
using System.Collections.Generic;

public class SpawnItemCommand : ICommand
{
    private CollectionPanel _collectionPanel;
    private LocationsManager _locationManager;
    
    private SpawnItemModel _model;

    public SpawnItemCommand(CollectionPanel collectionPanel, SpawnItemModel model, LocationsManager locationsManager)
    {
        _collectionPanel = collectionPanel;
        _model = model;
        _locationManager = locationsManager;
    }

    public event Action Completed;

    public void Execute()
    {
        _locationManager.TryGet(_model.Location, out ILocation location);
        location.Add(_model.Item);
        _collectionPanel.ShowItems(location.ItemsView);
        Completed?.Invoke();
    }
}