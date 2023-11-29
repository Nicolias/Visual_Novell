using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LocationsManager : MonoBehaviour, ISaveLoadObject
{
    [SerializeField] private List<LocationSO> _allLocationsSO = new List<LocationSO>();

    private SaveLoadServise _saveLoadServise;

    private Map _map;
    private List<ILocation> _locations = new List<ILocation>();

    private string _saveKey = "locationManagerKey";

    public IEnumerable<ILocation> AvailableLocations
    {
        get
        {
            List<ILocation> availableLocations = new List<ILocation>();

            foreach (var location in _locations)
                if (location.IsAvailable)
                    availableLocations.Add(location);

            return availableLocations;
        }
    }

    public IEnumerable<ILocation> AllLocations => _locations;

    [Inject]
    public void Construct(TimesOfDayServise timesOfDayServise, SaveLoadServise saveLoadServise,
        BackgroundView background,
        CollectionPanel collectionPanel, CharacterRenderer charactersPortraitView, Map map,
        CharactersLibrary charactersLibrary)
    {
        _saveLoadServise = saveLoadServise;
        _map = map;

        foreach (var locationSo in _allLocationsSO)
            locationSo.Initialize(timesOfDayServise);

        for (int i = 0; i < _allLocationsSO.Count; i++)
        {
            Location location = new Location(background, collectionPanel, charactersPortraitView, timesOfDayServise,
                saveLoadServise, _allLocationsSO[i], i);

            _locations.Add(location);
        }

        foreach (var character in charactersLibrary.AllCharacters)
            if (character.ScriptableObject.CurrentLocation.TryGet(timesOfDayServise.GetCurrentTimesOfDay(),
                out LocationSO location))
                _locations.FirstOrDefault(loc => loc.Data == location)?.Set(character.ScriptableObject);

        if (_saveLoadServise.HasSave(_saveKey))
            Load();

        _map.Add(_locations.TakeWhile(location => location.IsAvailable == true).ToList());
    }

    public void AddToMap(IEnumerable<LocationSO> newLocations)
    {
        _map.Add(Get(newLocations));
        _locations.AddRange(Get(newLocations));

        Save();
    }

    public void RemoveFromMap(IEnumerable<LocationSO> locations)
    {
        _map.Remove(Get(locations));

        foreach (var locationSO in locations)
            if(TryGet(locationSO, out ILocation location))
                _locations.Remove(location);

        Save();
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.BoolData() {Bool = true});

        for (int i = 0; i < _locations.Count; i++)
            _saveLoadServise.Save(_saveKey + i, new SaveData.BoolData() {Bool = _locations.Contains(_locations[i])});
    }

    public void Load()
    {
        for (int i = 0; i < _locations.Count; i++)
        {
            bool hasLocation = _saveLoadServise.Load<SaveData.BoolData>(_saveKey + i).Bool;

            if (hasLocation)
                _locations.Add(_locations[i]);
        }
    }

    public IEnumerable<ILocation> Get(IEnumerable<LocationSO> locationsSO)
    {
        List<ILocation> locations = new List<ILocation>();

        foreach (var locationSO in locationsSO)
            if (TryGet(locationSO, out ILocation location))
                locations.Add(location);

        return locations;
    }

    public bool TryGet(LocationSO locationSo, out ILocation location)
    {
        if (_locations.Exists(location => location.Data == locationSo))
        {
            location = _locations.FirstOrDefault(location => location.Data == locationSo);
            return true;
        }

        throw new InvalidOperationException();
    }
}
