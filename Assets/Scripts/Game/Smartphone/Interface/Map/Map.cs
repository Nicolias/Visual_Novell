using Factory.Cells;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using Zenject;

public class Map : WindowInSmartphone, ISaveLoadObject
{
    [SerializeField] private GameCommander _gameCommander;

    [SerializeField] private Transform _locationCellContainer;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Canvas _selfCanvas;

    [SerializeField] private GameObject _guidPanel;
    private bool _guidComplete;

    private SaveLoadServise _saveLoadServise;
    private ICellsFactory<Location> _locationCellFactory;
    private Smartphone _smartphone;
    private ChoicePanel _choicePanel;
    private LocationsManager _locationsManager;

    private List<Cell<Location>> _cells = new List<Cell<Location>>();
    private List<Location> _locations = new List<Location>();
    private Location _currentLocation;

    private const string _saveKey = "MapSave";

    public int LocationsCount => _locations.Count;
    public int LocationCellsCount => _cells.Count;

    [Inject]
    public void Construct(SaveLoadServise saveLoadServise, CellsFactoryCreater cellFactoryCreater,
        Smartphone smartphone, IChoicePanelFactory choicePanelFactory, LocationsManager locationsManager)
    {
        _saveLoadServise = saveLoadServise;
        _smartphone = smartphone;
        _locationsManager = locationsManager;
        _locationCellFactory = cellFactoryCreater.CreateCellsFactory<Location>();
        _choicePanel = choicePanelFactory.CreateChoicePanel(transform);
    }

    private void Awake()
    {
        _locationsManager.ConstructMap();

        if (_closeButton != null)
            _closeButton.onClick.AddListener(Hide);

        if (_saveLoadServise.HasSave(_saveKey))
        {
            Load();

            if (_currentLocation != null)
                ChangeLocation(_currentLocation);
        }
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

        _currentLocation = location;
        _currentLocation.QuestStarted += OnQuestStarted;

        _currentLocation.Show();
    }

    public void Add(IEnumerable<Location> locations)
    {
        List<Cell<Location>> newCells = _locationCellFactory.CreateCellsView(locations, _locationCellContainer);

        _locations.AddRange(locations);
        _cells.AddRange(newCells);

        foreach (var locationCell in _cells)
            locationCell.Clicked += OnLocationSelected;
    }

    public void Remove(IEnumerable<Location> locations)
    {
        foreach (var location in locations)
        {
            location.Disable();
            Cell<Location> locationCell = _cells.Find(locationCell => locationCell.Data == location);

            if (locationCell != null)
            {
                locationCell.Clicked -= OnLocationSelected;

                _locations.Remove(location);
                _cells.Remove(locationCell);
                locationCell.Dispose();
            }
        }
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

    protected override void OnEnabled()
    {
    }

    protected override void OnDisabled()
    {
        foreach (Transform locationCell in _locationCellContainer)
            Destroy(locationCell.gameObject);

        foreach (var locationCell in _cells)
            locationCell.Clicked -= OnLocationSelected;

        _closeButton.onClick.RemoveAllListeners();

        _locationsManager.Save();
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

    private void Show()
    {
        if (enabled == false)
            return;

        _selfCanvas.enabled = true;
    }

    private void Hide()
    {
        _selfCanvas.enabled = false;
        _choicePanel.Hide();
    }

    private void OnQuestStarted(Location location, Node quest)
    {
        _gameCommander.PackAndExecuteCommand(quest);
        location.RemoveQuest();
    }
}