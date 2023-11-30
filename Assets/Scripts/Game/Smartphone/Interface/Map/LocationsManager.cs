using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LocationsManager : MonoBehaviour
{
    [SerializeField] private List<LocationSO> _allLocationsSO = new List<LocationSO>();

    private List<ILocation> _locations = new List<ILocation>();
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
        BackgroundView background, CollectionPanel collectionPanel, CharacterRenderer charactersPortraitView)
    {
        foreach (var locationSo in _allLocationsSO)
            locationSo.Initialize(timesOfDayServise);

        for (int i = 0; i < _allLocationsSO.Count; i++)
        {
            Location location = new Location(background, collectionPanel, charactersPortraitView, timesOfDayServise,
                saveLoadServise, _allLocationsSO[i], i);

            _locations.Add(location);
        }
    }

    private void OnDestroy()
    {
        foreach(var location in _locations)
            location.Dispose();
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

    public ILocation GetBy(int id)
    {
        return _locations.Find(location => location.ID == id);
    }
}
