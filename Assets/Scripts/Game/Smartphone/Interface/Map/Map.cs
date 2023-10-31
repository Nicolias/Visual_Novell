using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using Zenject;

public class Map : MonoBehaviour, ISaveLoadObject
{
    [SerializeField] private GameCommander _gameCommander;

    [SerializeField] private Transform _locationCellContainer;
    [SerializeField] private Button _closeButton, _openButton;
    [SerializeField] private Canvas _selfCanvas;

    [SerializeField] private GameObject _guidPanel;
    private bool _guidComplete;

    private SaveLoadServise _saveLoadServise;
    private ILocationCellsFactory _locationCellFactory;
    private Smartphone _smartphone;
    private ChoicePanel _choicePanel;
    private LocationsManager _locationsManager;

    private List<LocationCell> _locationCells = new List<LocationCell>();
    private List<Location> _locations = new List<Location>();
    private Location _currentLocation;

    private const string _saveKey = "MapSave";

    public int LocationsCount => _locations.Count;
    public int LocationCellsCount => _locationCells.Count;

    [Inject]
    public void Construct(SaveLoadServise saveLoadServise, ILocationCellsFactory locationCellFactory,
        Smartphone smartphone, ChoicePanel choicePanel, LocationsManager locationsManager)
    {
        _saveLoadServise = saveLoadServise;
        _locationCellFactory = locationCellFactory;
        _smartphone = smartphone;
        _choicePanel = choicePanel;
        _locationsManager = locationsManager;

        _locationsManager.ConstructMap();

        _closeButton.onClick.AddListener(Hide);
        _openButton.onClick.AddListener(Show);

        if (_saveLoadServise.HasSave(_saveKey))
        {
            Load();

            if (_currentLocation != null)
                ChangeLocation(_currentLocation);
        }
    }

    private void OnDestroy()
    {
        Save();

        foreach (Transform locationCell in _locationCellContainer)
            Destroy(locationCell.gameObject);

        foreach (var locationCell in _locationCells)
            locationCell.LocationSelected -= OnLocationSelected;

        _openButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();

        _locationsManager.Save();
    }

    public void Show()
    {
        if (enabled == false)
            return;

        _selfCanvas.enabled = true;
    }

    public void SetEnable(bool eneble)
    {
        _openButton.enabled = eneble;

        if (_guidComplete == false)
        {
            _guidPanel.SetActive(true);
            _guidComplete = true;
        }
    }

    public void ChangeLocation(Location location)
    {
        if (_currentLocation != null)
        {
            _currentLocation.Dispose();
            _currentLocation.QuestStarted -= OnQuestStarted;
        }

        _currentLocation = location;
        _currentLocation.QuestStarted += OnQuestStarted;

        _currentLocation.Show();
    }

    public void Add(IEnumerable<Location> locations)
    {
        List<LocationCell> newCells = _locationCellFactory.CreateNewLocationCell(locations, _locationCellContainer);

        _locations.AddRange(locations);
        _locationCells.AddRange(newCells);

        foreach (var locationCell in _locationCells)
            locationCell.LocationSelected += OnLocationSelected;
    }

    public void Remove(IEnumerable<Location> locations)
    {
        foreach (var location in locations)
        {
            location.Disable();
            LocationCell locationCell = _locationCells.Find(locationCell => locationCell.Location == location);
            locationCell.LocationSelected -= OnLocationSelected;

            _locations.Remove(location);
            _locationCells.Remove(locationCell);
            DestroyImmediate(locationCell.gameObject);
        }
    }

    private void OnLocationSelected(Location location)
    {
        _choicePanel.Show($"Перейти на локацию {location.Name}", new List<ChoiseElement>()
        {
            new ChoiseElement("Подвердить", () => 
            {
                ChangeLocation(location);

                Hide();
                _choicePanel.Hide();
                _smartphone.Hide();
            })
        });
    }

    private void Hide()
    {
        _selfCanvas.enabled = false;
        _choicePanel.Hide();
    }

    private void OnQuestStarted(Node quest)
    {
        _gameCommander.PackAndExecuteCommand(quest);
    }

    public void Save()
    {
        //for (int i = 0; i < _locations.Count; i++)
        //{
        //    List<ItemForCollection> itemsOnLocation = new();
        //    itemsOnLocation.AddRange(_locations[i].Items);

        //    _saveLoadServise.Save($"{_saveKey}/{i}", new SaveData.IntData() { Int = itemsOnLocation.Count });

        //    for (int k = 0; k < itemsOnLocation.Count; k++)
        //    {
        //        _saveLoadServise.Save($"{_saveKey}/{i}/{k}", new SaveData.LocationData()
        //        {
        //            Quest = _locations[i].QuestOnLocation,
        //            Items = itemsOnLocation[k]
        //        });
        //    }
        //}

        int currentLocatoinIndex = -1;

        if (_currentLocation != null)
            currentLocatoinIndex = _locations.IndexOf(_currentLocation);

        _saveLoadServise.Save(_saveKey, new SaveData.MapData()
        {
            Enabled = _openButton.enabled,
            GuidComplete = _guidComplete,
            CurrentLocationIndex = currentLocatoinIndex
        });
    }

    public void Load()
    {
        var data = _saveLoadServise.Load<SaveData.MapData>(_saveKey);

        _openButton.enabled = data.Enabled;

        if (data.CurrentLocationIndex != -1)
            _currentLocation = _locations[data.CurrentLocationIndex];

        _guidComplete = data.GuidComplete;

        //for (int i = 0; i < _locations.Count; i++)
        //{
        //    int count = _saveLoadServise.Load<SaveData.IntData>($"{_saveKey}/{i}").Int;

        //    for (int k = 0; k < count; k++)
        //    {
        //        _locations[i].Set(_saveLoadServise.Load<SaveData.LocationData>($"{_saveKey}/{i}/{k}").Quest);
        //        var locationData = _saveLoadServise.Load<SaveData.LocationData>($"{_saveKey}/{i}/{k}");
        //        var item = locationData.Items;

        //        if (_locations[i].Contains(item) == false)
        //            _locations[i].Add(item);
        //    }
        //}
    }
}