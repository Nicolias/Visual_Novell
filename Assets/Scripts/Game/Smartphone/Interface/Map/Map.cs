using Factory.Cells;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using Zenject;

public class Map : WindowInSmartphone, ISaveLoadObject
{
    [SerializeField] private Transform _containerForCells;

    [SerializeField] private GameCommander _gameCommander;

    [SerializeField] private Button _closeButton;
    [SerializeField] private Canvas _selfCanvas;

    [SerializeField] private GameObject _guidPanel;
    private bool _guidComplete;

    private LocationCellsContainer _cellsCreater;
    private SaveLoadServise _saveLoadServise;
        
    private const string _saveKey = "MapSave";

    private List<Location> _locations = new List<Location>();
    private Location _currentLocation;

    public int LocationsCount => _locations.Count;
    public int LocationCellsCount => _cellsCreater.CellsCount;

    [Inject]
    public void Construct(SaveLoadServise saveLoadServise, CellsFactoryCreater cellFactoryCreater,
        Smartphone smartphone, IChoicePanelFactory choicePanelFactory)
    {
        _cellsCreater = new LocationCellsContainer(choicePanelFactory.CreateChoicePanel(transform), smartphone);

        _saveLoadServise = saveLoadServise;
        _cellsCreater.SetCellsFactory(cellFactoryCreater.CreateCellsFactory<Location>(), _containerForCells);
    }

    private void Awake()
    {
        //_locationsManager.ConstructMap();

        if (_closeButton != null)
            _closeButton.onClick.AddListener(Hide);

        if (_saveLoadServise.HasSave(_saveKey))
        {
            Load();

            if (_currentLocation != null)
                ChangeLocation(_currentLocation);
        }

        _cellsCreater.CellClicked += ChangeLocation;
    }

    public void Add(IEnumerable<Location> locations)
    {
        _locations.AddRange(locations);
        _cellsCreater.Add(locations);
    }

    public void Remove(IEnumerable<Location> locations)
    {
        foreach (var location in locations)
            _locations.Remove(location);

        _cellsCreater.Remove(locations);
    }

    public override void SetEnabled(bool enabled)
    {
        base.SetEnabled(enabled);

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

        Hide();

        _currentLocation = location;
        _currentLocation.QuestStarted += OnQuestStarted;

        _currentLocation.Show();
    }

    public void Save()
    {
        int currentLocatoinIndex = -1;

        if (_currentLocation != null)
            currentLocatoinIndex = _locations.IndexOf(_currentLocation);

        _saveLoadServise.Save(_saveKey, new SaveData.MapData()
        {
            Enabled = OpenButton.enabled,
            GuidComplete = _guidComplete,
            CurrentLocationIndex = currentLocatoinIndex
        });
    }

    public void Load()
    {
        var data = _saveLoadServise.Load<SaveData.MapData>(_saveKey);

        OpenButton.enabled = data.Enabled;

        if (data.CurrentLocationIndex != -1)
            _currentLocation = _locations[data.CurrentLocationIndex];

        _guidComplete = data.GuidComplete;
    }

    protected override void OnOpenButtonClicked()
    {
        Show();
    }

    protected override void OnDisabled()
    {
        _cellsCreater.Dispose();
        _closeButton.onClick.RemoveAllListeners();
        _cellsCreater.CellClicked -= ChangeLocation;

        Save();
    }    

    private void Show()
    {
        if (enabled == false)
            return;

        _selfCanvas.enabled = true;
    }

    private void Hide()
    {
        _selfCanvas.enabled = false;
        _cellsCreater.HideChoicePanel();
    }

    private void OnQuestStarted(Location location, Node quest)
    {
        _gameCommander.PackAndExecuteCommand(quest);
        location.RemoveQuest();
    }
}

public class LocationCellsContainer
{
    private ICellsFactory<Location> _cellsFactory;
    private Transform _cellsContainer;

    private Dictionary<Superlocation, CellView> _supperLocations = new Dictionary<Superlocation, CellView>();
    private List<Cell<Location>> _locationCells = new List<Cell<Location>>();

    private ChoicePanel _choicePanel;
    private Smartphone _smartphone;

    public LocationCellsContainer(ChoicePanel choicePanel, Smartphone smartphone)
    {
        _choicePanel = choicePanel;
        _smartphone = smartphone;
    }

    public int CellsCount => _locationCells.Count;

    public event Action<Location> CellClicked;

    public void SetCellsFactory(ICellsFactory<Location> cellsFactory, Transform cellsContainer)
    {
        _cellsFactory = cellsFactory;
        _cellsContainer = cellsContainer;
    }

    public void Dispose()
    {
        foreach (var locationCell in _locationCells)
        {
            locationCell.Clicked -= OnLocationSelected;
            locationCell.Dispose();
        }
    }

    public void HideChoicePanel()
    {
        _choicePanel.Hide();
    }

    public void ShowAllCells()
    {
        foreach (var supperLocation in _supperLocations)
            supperLocation.Value.ChangeSubcellsEnable(false);
    }

    public void Add(IEnumerable<Location> locations)
    {
        foreach (var location in locations)
        {
            TryCreateSupppercell(location);

            Cell<Location> newLocationCell = CreateCellView(location, location.Superlocation != null ? 
                _supperLocations[location.Superlocation].SubcellsContainer : _cellsContainer);

            _locationCells.Add(newLocationCell);
        }

        foreach (var locationCell in _locationCells)
            locationCell.Clicked += OnLocationSelected;
    }

    public void Remove(IEnumerable<Location> locations)
    {
        foreach (var location in locations)
        {
            location.Disable();
            Cell<Location> locationCell = _locationCells.Find(locationCell => locationCell.Data == location);

            if (locationCell != null)
            {
                locationCell.Clicked -= OnLocationSelected;

                
                _locationCells.Remove(locationCell);
                locationCell.Dispose();
            }
        }
    }

    private bool TryCreateSupppercell(Location location)
    {
        if (location.Superlocation != null)
        {
            SupperCell<Location> supperCell = _cellsFactory.CreateSupperCellView(location, _cellsContainer);

            if (_supperLocations.ContainsKey(location.Superlocation) == false)
                _supperLocations.Add(location.Superlocation, supperCell.Veiw);

            return true;
        }

        return false;
    }

    private Cell<Location> CreateCellView(Location location, Transform cellContainer)
    {
        return _cellsFactory.CreateCellsView(new List<Location>() { location }, cellContainer)[0];
    }

    private void OnLocationSelected(Location location)
    {
        _choicePanel.Show($"Перейти на локацию {location.Name}", new List<ChoiseElement>()
        {
            new ChoiseElement("Подвердить", () =>
            {
                CellClicked?.Invoke(location);

                HideChoicePanel();
                _smartphone.Hide();
            })
        });
    }
}