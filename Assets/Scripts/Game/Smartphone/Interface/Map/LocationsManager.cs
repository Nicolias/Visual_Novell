using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using StateMachine;

public class LocationsManager : MonoBehaviour, IByStateMachineChangable, ISaveLoadObject
{
    [SerializeField] private List<LocationSO> _allLocationsSO = new List<LocationSO>();

    private List<ILocation> _locations = new List<ILocation>();

    private GameStateMachine _gameStateMachine;
    private GameStateVisitor _gameStateVisitor;

    private CharactersLibrary _characterLibrary;
    private BackgroundView _background;
    private CollectionPanel _collectionPanel;
    private CharacterRenderer _charactersPortraitView;
    private TimesOfDayServise _timesOfDayServise;
    private SaveLoadServise _saveLoadServise;

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
    public void Construct(GameStateMachine gameStateMachine, TimesOfDayServise timesOfDayServise, SaveLoadServise saveLoadServise,
        BackgroundView background, CollectionPanel collectionPanel, CharacterRenderer charactersPortraitView, CharactersLibrary charactersLibrary)
    {
        foreach (var locationSo in _allLocationsSO)
            locationSo.Initialize(timesOfDayServise);

        _timesOfDayServise = timesOfDayServise;
        _characterLibrary = charactersLibrary;
        _gameStateMachine = gameStateMachine;
        _saveLoadServise = saveLoadServise;
        _background = background;
        _collectionPanel = collectionPanel;
        _charactersPortraitView = charactersPortraitView;

        _gameStateVisitor = new GameStateVisitor(gameStateMachine, this);
        _gameStateVisitor.RecognizeCurrentGameState();
        _gameStateVisitor.SubscribeOnGameStateMachine();

        for (int i = 0; i < _allLocationsSO.Count; i++)
        {
            Location location = new Location(_gameStateMachine, _background, _collectionPanel, _charactersPortraitView, _timesOfDayServise,
                _saveLoadServise, _allLocationsSO[i], i);

            _locations.Add(location);
        }

        Add();
    }

    private void OnDestroy()
    {
        foreach(var location in _locations)
            location.Dispose();

        _gameStateVisitor.UnsubsciribeFromGameStateMachine();
    }

    void IByStateMachineChangable.ChangeBehaviourBy(PlayState playState)
    {
        foreach (var character in _characterLibrary.AllCharacters)
            if (character.ScriptableObject.CurrentLocation.TryGet(_timesOfDayServise.GetCurrentTimesOfDay(), out LocationSO locationSO))
                if (Get(locationSO, out ILocation location))
                    location.Set(character.ScriptableObject);
    }

    void IByStateMachineChangable.ChangeBehaviourBy(StoryState storyState)
    {
    }

    public IEnumerable<ILocation> Get(IEnumerable<LocationSO> locationsSO)
    {
        List<ILocation> locations = new List<ILocation>();

        foreach (var locationSO in locationsSO)
            if (Get(locationSO, out ILocation location))
                locations.Add(location);

        return locations;
    }

    public bool Get(LocationSO locationSo, out ILocation location)
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

    public void Add()
    {
        _saveLoadServise.Add(this);
    }

    public void Save()
    {
        _locations.ForEach(location => location.Save());
    }

    public void Load()
    {
        _locations.ForEach(_location => _location.Load());
    }
}
