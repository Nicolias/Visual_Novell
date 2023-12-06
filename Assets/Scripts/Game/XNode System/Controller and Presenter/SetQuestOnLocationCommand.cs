using System;
using Characters;

public class SetQuestOnLocationCommand : ICommand
{
    private SetQuestOnLocationModel _model;
    private LocationsManager _locationManager;

    public SetQuestOnLocationCommand(SetQuestOnLocationModel model, LocationsManager locationsManager)
    {
        _model = model;
        _locationManager = locationsManager;
    }

    public event Action Completed;

    public void Execute()
    {
        _locationManager.TryGet(_model.Location, out ILocation location);
        location.Set(_model.Quest);
        Completed?.Invoke();
    }
}
