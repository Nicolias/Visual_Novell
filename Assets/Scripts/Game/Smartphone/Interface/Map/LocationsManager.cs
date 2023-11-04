using System;
using System.Collections.Generic;

public class LocationsManager : ISaveLoadObject
{
    private readonly SaveLoadServise _saveLoadServise;

    private readonly CharacterRenderer _charactersPortraitView;
    private readonly BackgroundView _background;
    private readonly CollectionPanel _collectionPanel;
    private readonly Map _map;
    private readonly List<Location> _locations;

    private List<Location> _locationsInMap = new List<Location>();

    private string _saveKey = "locationManagerKey";

    public LocationsManager(SaveLoadServise saveLoadServise, BackgroundView background,
        CollectionPanel collectionPanel, CharacterRenderer charactersPortraitView, Map map, List<Location> locations)
    {
        _saveLoadServise = saveLoadServise;

        _charactersPortraitView = charactersPortraitView;
        _background = background;
        _collectionPanel = collectionPanel;
        _map = map;
        _locations = locations;

        if(_saveLoadServise.HasSave(_saveKey))
            Load();
    }
    
    public void ConstructMap()
    {
        foreach (var location in _locations)
            location.Initialize(_background, _collectionPanel, _charactersPortraitView);

        List<Location> locations = new List<Location>(_locationsInMap);

        for (int i = 0; i < _locations.Count; i++)
            if (_locations[i].IsAvilable == false)
                locations.Remove(_locations[i]);

        _map.Add(_locationsInMap);
    }

    public void AddToMap(IEnumerable<Location> locations)
    {
        foreach (var location in locations)
            if (_locationsInMap.Contains(location))
                return;

        _map.Add(locations);

        _locationsInMap.AddRange(locations);
    }

    public void RemoveFromMap(IEnumerable<Location> locations)
    {
        _map.Remove(locations);

        foreach (var location in locations)
            _locationsInMap.Remove(location);
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.BoolData() { Bool = true });

        for (int i = 0; i < _locations.Count; i++)
        {
            if(_locationsInMap.Contains(_locations[i]))
                _saveLoadServise.Save(_saveKey + i, new SaveData.BoolData() { Bool = true});
            else
                _saveLoadServise.Save(_saveKey + i, new SaveData.BoolData() { Bool = false });
        }
    }

    public void Load()
    {
        for (int i = 0; i < _locations.Count; i++)
        {
            bool hasLocation = _saveLoadServise.Load<SaveData.BoolData>(_saveKey + i).Bool;

            if(hasLocation)
                _locationsInMap.Add(_locations[i]);
        }            
    }
}
