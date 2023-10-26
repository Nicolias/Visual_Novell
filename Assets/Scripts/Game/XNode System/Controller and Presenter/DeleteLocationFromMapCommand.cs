using System;
using System.Collections.Generic;

public class DeleteLocationFromMapCommand : ICommand
{
    private List<Location> _locations;
    private readonly Map _map;

    public DeleteLocationFromMapCommand(List<Location> locations, Map map)
    {
        _locations = locations;
        _map = map;
    }

    public event Action Complete;

    public void Execute()
    {
        foreach (var location in _locations)
            _map.Remove(location);

        Complete?.Invoke();
    }
}