using Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using SaveData;
using UnityEngine;
using XNode;

[CreateAssetMenu(fileName = "New location", menuName = "Location/Location")]
public class LocationSO : ScriptableObject, IDataForCell
{
    [SerializeField] private Dictionary.Dictionary<TimesOfDayType, Vector2> _locationOffsetByDeyTime;
    [SerializeField] private Dictionary.Dictionary<TimesOfDayType, Sprite> _locationSpritesByDeyTime;

    [SerializeField] private List<Vector2> _artifactPositionVariations;
    [SerializeField] private List<ItemForCollection> _itemsOnLocation = new List<ItemForCollection>();

    [Header("Настройки положения персонажа на локации")]
    [SerializeField] private Vector2 _characterPosition;
    [SerializeField] private Vector3 _characterScale;

    [SerializeField] private CharacterPoseType _characterPoseType;
    [Header("")]

    [SerializeField] private List<PastimeOnLocationType> _actionsOnLocation;
    
    private TimesOfDayServise _timesOfDayServise;

    [field: SerializeField] public bool IsForArtifacts { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Superlocation Superlocation { get; private set; }

    public bool IsAvailable => _locationSpritesByDeyTime.Contains(_timesOfDayServise.GetCurrentTimesOfDay());

    public IEnumerable<ItemForCollection> ItemsOnLocation => _itemsOnLocation;
    public IEnumerable<PastimeOnLocationType> ActionsList => _actionsOnLocation;

    public void Initialize(TimesOfDayServise timesOfDayServise)
    {
        _timesOfDayServise = timesOfDayServise;
    }

    public CharacterOnLocationData Get(CharacterSO character)
    {
        Vector2 characterOffset = _characterPosition;

        if (_locationOffsetByDeyTime.TryGet(_timesOfDayServise.GetCurrentTimesOfDay(), out Vector2 characterOffsetByDayTime))
            characterOffset += characterOffsetByDayTime;

        if (character.Images.TryGet(_characterPoseType, out Sprite characterSprite))
        {
            return new CharacterOnLocationData(character.Type, character.Name, characterSprite, this,
                    _characterPoseType == CharacterPoseType.Staying ? CharacterPortraitPosition.Right : CharacterPortraitPosition.FreePosition,
                    characterOffset, _characterScale);
        }
        else
        {
            throw new InvalidOperationException("Эта локация не предназначена для приглашения.");
        }
    }

    public bool TryGetByCurrentTime(out Sprite sprite)
    {
        return _locationSpritesByDeyTime.TryGet(_timesOfDayServise.GetCurrentTimesOfDay(), out sprite);
    }

    public Vector2 GetRandomArtifactPosition()
    {
        return _artifactPositionVariations[UnityEngine.Random.Range(0, _artifactPositionVariations.Count)];
    }

    public void Destory(IEnumerable<ItemForCollectionView> itemsView)
    {
        foreach (var itemView in itemsView)
            if(itemView is Artifact)
                Destroy(itemView.gameObject);
    }
}

public class Location : ILocation
{
    private readonly LocationSO _locationSO;

    private readonly LocationSaveLoader _locationSaveLoader;
    private readonly CharacterRenderer _charactersRenderer;
    private readonly BackgroundView _background;
    private readonly CollectionPanel _collectionPanel;
    private readonly TimesOfDayServise _timesOfDayServise;

    private readonly int _id;

    private CharacterSO _characterOnLocation;
    private Node _questOnLocation;

    private List<ItemForCollection> _itemsOnLocation = new List<ItemForCollection>();
    private List<ItemForCollectionView> _itemsView = new List<ItemForCollectionView>();

    public LocationSO Data => _locationSO;
    public bool IsForArtifacts => _locationSO.IsForArtifacts;
    public bool IsAvailable => _locationSO.IsAvailable;

    public int ID => _id;

    public string Name => _locationSO.Name;
    public Superlocation Superlocation => _locationSO.Superlocation;

    public Node CurrentQuest => _questOnLocation;

    public IEnumerable<ItemForCollection> ItemsOnLocationData => _itemsOnLocation;
    public IEnumerable<ItemForCollectionView> ItemsView => _itemsView;

    public event Action<ILocation, Node> QuestStarted;

    public Location(BackgroundView background, CollectionPanel collectionPanel,
        CharacterRenderer charactersPortraitView, TimesOfDayServise timesOfDayServise,
        SaveLoadServise saveLoadServise, LocationSO locationSo, int id)
    {
        _locationSO = locationSo;
        
        _background = background;
        _collectionPanel = collectionPanel;
        _charactersRenderer = charactersPortraitView;
        _timesOfDayServise = timesOfDayServise;

        _id = id;
        _locationSaveLoader = new LocationSaveLoader(saveLoadServise, this, id);

        _locationSaveLoader.Load();
        
        if(_itemsOnLocation.Count == 0)
            _itemsOnLocation.AddRange(_locationSO.ItemsOnLocation);
        
        _itemsView = collectionPanel.CreateItemsView(_itemsOnLocation);
    }

    public override string ToString()
    {
        return Name;
    }

    public void Load(LocationData data)
    {
        _questOnLocation = data.Quest;
        _itemsOnLocation.AddRange(data.Items);
        _itemsOnLocation.AddRange(_locationSO.ItemsOnLocation);
    }
    
    public void Show()
    {
        _charactersRenderer.DeleteAllCharacters();

        if (_questOnLocation != null)
            QuestStarted?.Invoke(this, _questOnLocation);

        if (_locationSO.TryGetByCurrentTime(out Sprite sprite))
        {
            _background.Replace(sprite);
            CheckCorrectCharacterOnLocation();

            if (_characterOnLocation != null)
                _charactersRenderer.Show(_locationSO.Get(_characterOnLocation));
        }

        _collectionPanel.ShowItems(_itemsView);
        _collectionPanel.ItemDeleted += OnItemDelete;
    }

    public void Dispose()
    {
        if (_collectionPanel != null)
            _collectionPanel.ItemDeleted -= OnItemDelete;

        _locationSaveLoader.Save();
    }

    public void Add(ItemForCollection artifact)
    {
        _itemsOnLocation.Add(artifact);

        Vector2 spawnPosition = artifact is Artifact == true ? _locationSO.GetRandomArtifactPosition() : artifact.ItemAfterInstantiatePosition;

        _itemsView.Add(_collectionPanel.CreateItemsView(artifact, spawnPosition));
    }

    public void DeleteArtifacts()
    {
        _itemsOnLocation.Clear();
        
        _locationSO.Destory(_itemsView);
        _itemsView.Clear();
    }

    public void Set(CharacterSO character)
    {
        _characterOnLocation = character;
    }

    public void Set(Node questOnLocation)
    {
        _questOnLocation = questOnLocation;
    }

    public void RemoveQuest()
    {
        _questOnLocation = null;
    }

    private void OnItemDelete(ItemForCollection itemData)
    {
        _itemsOnLocation.Remove(itemData);
        _itemsView.RemoveAll(x => x == null);
    }

    private void CheckCorrectCharacterOnLocation()
    {
        if (_characterOnLocation != null)
        {
            _characterOnLocation.CurrentLocation.TryGet(_timesOfDayServise.GetCurrentTimesOfDay(), out LocationSO locationSO);

            if (locationSO != _locationSO)
                _characterOnLocation = null;
        }
    }
}

public interface ILocation : IDataForCell, IDisposable
{
    public LocationSO Data { get; }
    
    public bool IsAvailable { get; }

    public int ID { get; }
    public IEnumerable<ItemForCollectionView> ItemsView { get; }

    public event Action<ILocation, Node> QuestStarted;

    public void Show();
    
    public void Add(ItemForCollection artifact);

    public void DeleteArtifacts();

    public void Set(CharacterSO character);

    public void Set(Node questOnLocation);

    public void RemoveQuest();
}

public class LocationSaveLoader : ISaveLoadObject
{
    private SaveLoadServise _saveLoadServise;
    private Location _location;
    private string _saveKey;

    public LocationSaveLoader(SaveLoadServise saveLoadServise, Location location, int id)
    {
        _saveLoadServise = saveLoadServise;
        _location = location;
        _saveKey = $"Location{id}";
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.LocationData()
        {
            Quest = _location.CurrentQuest,
            Items = _location.ItemsOnLocationData.ToList()
        });
    }

    public void Load()
    {
        if (_saveLoadServise.HasSave(_saveKey) == false) return;
        
        _location.Load(_saveLoadServise.Load<SaveData.LocationData>(_saveKey));
    }
}