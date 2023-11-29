using System;
using System.Collections.Generic;

public class LocationsManager : ISaveLoadObject
{
    private readonly SaveLoadServise _saveLoadServise;

    private readonly Map _map;
    private readonly List<Location> _locations;

    private List<Location> _locationsInMap = new List<Location>();

    private string _saveKey = "locationManagerKey";

    public IEnumerable<Location> AvailableLocations
    {
        get
        {
            List<Location> availableLocations = new List<Location>();

            foreach (var location in _locationsInMap)
                if (location.IsAvailable)
                    availableLocations.Add(location);

            return availableLocations;
        }
    }
    public IEnumerable<Location> AllLocations => _locations;

    public LocationsManager(TimesOfDayServise timesOfDayServise, SaveLoadServise saveLoadServise, BackgroundView background,
        CollectionPanel collectionPanel, CharacterRenderer charactersPortraitView, Map map, CharactersLibrary charactersLibrary,
        List<Location> locations)
    {
        foreach (var character in charactersLibrary.AllCharacters)
            if (character.ScriptableObject.CurrentLocation.TryGet(timesOfDayServise.GetCurrentTimesOfDay(), out Location location))
                location.Set(character.ScriptableObject);

        _saveLoadServise = saveLoadServise;

        _map = map;
        _locations = locations;

        if(_saveLoadServise.HasSave(_saveKey))
            Load();

        foreach (var location in _locations)
        {
            if (location == null)
                throw new InvalidOperationException("Локация не проинициализированна");

            location.Initialize(background, collectionPanel, charactersPortraitView, timesOfDayServise);
        }

        _map.Add(_locationsInMap);
    }      

    public void AddToMap(IEnumerable<Location> newLocations)
    {
        List<Location> locations = new List<Location>();

        foreach (var location in newLocations)
            if (_locationsInMap.Contains(location) == false)
                locations.Add(location);

        _map.Add(locations);
        _locationsInMap.AddRange(locations);

        Save();
    }

    public void RemoveFromMap(IEnumerable<Location> locations)
    {
        _map.Remove(locations);

        foreach (var location in locations)
            _locationsInMap.Remove(location);

        Save();
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.BoolData() { Bool = true });

        for (int i = 0; i < _locations.Count; i++)
            _saveLoadServise.Save(_saveKey + i, new SaveData.BoolData() { Bool = _locationsInMap.Contains(_locations[i]) });
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
