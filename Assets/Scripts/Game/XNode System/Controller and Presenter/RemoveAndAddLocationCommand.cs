using System;

public class RemoveAndAddLocationCommand : ICommand
{
    private readonly RemoveOrAddLocation _model;
    private readonly LocationsManager _locationsManager;
    private readonly Map _map;

    public RemoveAndAddLocationCommand(RemoveOrAddLocation model, LocationsManager locationsManager, Map map)
    {
        _model = model;
        _locationsManager = locationsManager;
        _map = map;
    }

    public event Action Completed;

    public void Execute()
    {
        _map.Add(_locationsManager.Get(_model.LocationsForAdd));
        _map.Remove(_locationsManager.Get(_model.LocationsForRemove));

        Completed?.Invoke();
    }
}