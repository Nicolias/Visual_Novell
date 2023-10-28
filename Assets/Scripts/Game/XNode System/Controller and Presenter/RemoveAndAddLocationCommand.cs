using System;

public class RemoveAndAddLocationCommand : ICommand
{
    private readonly RemoveOrAddLocation _model;
    private readonly LocationsManager _locationsManager;

    public RemoveAndAddLocationCommand(RemoveOrAddLocation model, LocationsManager locationsManager)
    {
        _model = model;
        _locationsManager = locationsManager;
    }

    public event Action Complete;

    public void Execute()
    {
        _locationsManager.AddToMap(_model.LocationsForAdd);
        _locationsManager.RemoveFromMap(_model.LocationsForRemove);

        Complete?.Invoke();
    }
}