using Factory.Cells;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using Zenject;
using StateMachine;

public class Map : WindowInSmartphone, ISaveLoadObject, IByStateMachineChangable
{
    [SerializeField] private Transform _containerForCells;

    [SerializeField] private GameCommander _gameCommander;
    [SerializeField] private CharacterRenderer _characterRenderer;

    [SerializeField] private Button _closeButton;
    [SerializeField] private Canvas _selfCanvas;

    private LocationsManager _locationManager;
    private LocationCellsContainer _cellsCreater;
    private SaveLoadServise _saveLoadServise;

    private GameStateVisitor _gameStateVisitor;

    private const string _saveKey = "MapSave";

    private List<ILocation> _locations = new List<ILocation>();
    private ILocation _currentLocation;

    public int LocationsCount => _locations.Count;
    public int LocationCellsCount => _cellsCreater.CellsCount;

    [Inject]
    public void Construct(SaveLoadServise saveLoadServise, CellsFactoryCreater cellFactoryCreater,
        LocationsManager locationsManager, Smartphone smartphone, IChoicePanelFactory choicePanelFactory, GameStateMachine gameStateMachine)
    {
        _locationManager = locationsManager;
        _saveLoadServise = saveLoadServise;

        _gameStateVisitor = new GameStateVisitor(gameStateMachine, this);
        _gameStateVisitor.RecognizeCurrentGameState();

        _cellsCreater = new LocationCellsContainer(choicePanelFactory.CreateChoicePanel(transform), smartphone);

        _cellsCreater.SetCellsFactory(cellFactoryCreater, _containerForCells);
    }

    private void Start()
    {
        if (_closeButton != null)
            _closeButton.onClick.AddListener(Hide);

        if (_saveLoadServise.HasSave(_saveKey))
        {
            Load();

            if (_currentLocation != null)
                ChangeLocation(_currentLocation);

            _cellsCreater.Add(_locations);
        }

        _cellsCreater.CellClicked += OnCellClicked;
        _gameStateVisitor.SubscribeOnGameStateMachine();

        Add();
    }

    void ISaveLoadObject.Save()
    {
        _saveLoadServise.Save(_saveKey + "LocatoinCount", new SaveData.IntData() { Int = _locations.Count });

        for (int i = 0; i < _locations.Count; i++)
            _saveLoadServise.Save(_saveKey + i, new SaveData.IntData() { Int = _locations[i].ID });

        int currentLocatoinIndex = -1;

        if (_currentLocation != null)
            currentLocatoinIndex = _locations.IndexOf(_currentLocation);

        _saveLoadServise.Save(_saveKey, new SaveData.MapData()
        {
            CurrentLocationIndex = currentLocatoinIndex
        });
    }

    void IByStateMachineChangable.ChangeBehaviourBy(PlayState playState)
    {
        _cellsCreater.CheckIntaractable();
    }

    void IByStateMachineChangable.ChangeBehaviourBy(StoryState storyState)
    {
    }

    public void Load()
    {
        int locationsCount = _saveLoadServise.Load<SaveData.IntData>(_saveKey + "LocatoinCount").Int;

        for (int i = 0; i < locationsCount; i++)
            _locations.Add(_locationManager.GetBy(_saveLoadServise.Load<SaveData.IntData>(_saveKey + i).Int));

        var data = _saveLoadServise.Load<SaveData.MapData>(_saveKey);

        if (data.CurrentLocationIndex != -1)
            _currentLocation = _locations[data.CurrentLocationIndex];
    }

    public void Add()
    {
        _saveLoadServise.Add(this);
    }

    public void Add(IEnumerable<ILocation> locations)
    {
        _locations.AddRange(locations);
        _cellsCreater.Add(locations);
    }

    public void Remove(IEnumerable<ILocation> locations)
    {
        foreach (var location in locations)
            _locations.Remove(location);

        _cellsCreater.Remove(locations);
    }

    public void ChangeLocation(LocationSO locationSo)
    {    
        if(_locationManager.Get(locationSo, out ILocation location))
            ChangeLocation(location);
    }
    
    public void ChangeLocation(ILocation location)
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

    protected override void OnOpenButtonClicked()
    {
        Show();
    }

    protected override void OnDisabled()
    {
        _cellsCreater.Dispose();
        _closeButton.onClick.RemoveAllListeners();

        _cellsCreater.CellClicked -= OnCellClicked;
        _gameStateVisitor.UnsubsciribeFromGameStateMachine();
    }

    private void Show()
    {
        if (enabled == false)
            return;

        _selfCanvas.enabled = true;

        _cellsCreater.HideAllSubcells();
    }

    private void Hide()
    {
        _selfCanvas.enabled = false;
        _cellsCreater.HideChoicePanel();
    }

    private void OnQuestStarted(ILocation location, Node quest)
    {
        _gameCommander.PackAndExecuteCommand(quest);
        location.RemoveQuest();
    }

    private void OnCellClicked(ILocation location)
    {
        ChangeLocation(location);
    }
}

public class LocationCellsContainer
{
    private ICellsFactory<ILocation> _cellsFactory;
    private ISuppercellsFactory<Superlocation> _supperCellsFactory;
    private Transform _cellsContainer;

    private Dictionary<Superlocation, SupperCell<Superlocation>> _supperlocationCells = new Dictionary<Superlocation, SupperCell<Superlocation>>();
    private List<Cell<ILocation>> _locationCells = new List<Cell<ILocation>>();

    private ChoicePanel _choicePanel;
    private Smartphone _smartphone;

    private CellView _currentOpenedSuppercell;

    public LocationCellsContainer(ChoicePanel choicePanel, Smartphone smartphone)
    {
        _choicePanel = choicePanel;
        _smartphone = smartphone;
    }

    public int CellsCount => _locationCells.Count;

    public event Action<ILocation> CellClicked;

    public void SetCellsFactory(CellsFactoryCreater cellsFactoryCreater, Transform cellsContainer)
    {
        _cellsFactory = cellsFactoryCreater.CreateCellsFactory<ILocation>();
        _supperCellsFactory = cellsFactoryCreater.CreateSuppercellsFactory<Superlocation>();
        _cellsContainer = cellsContainer;
    }

    public void Dispose()
    {
        foreach (var locationCell in _locationCells)
        {
            locationCell.Clicked -= OnLocationSelected;
            locationCell.Dispose();
        }
        _locationCells.Clear();

        foreach (var suppercell in _supperlocationCells)
        {
            suppercell.Value.Clicked -= OnSuppercellCliced;
            suppercell.Value.Dispose();
        }
        _supperlocationCells.Clear();
    }

    public void HideChoicePanel()
    {
        _choicePanel.Hide();
    }

    public void CheckIntaractable()
    {
        foreach (var locationCell in _locationCells)
            locationCell.SetInteractable(locationCell.Data.IsAvailable);
    }

    public void HideAllSubcells()
    {
        foreach (var supperLocation in _supperlocationCells)
            supperLocation.Value.View.ChangeSubcellsEnable(false);
    }

    public void Add(IEnumerable<ILocation> locations)
    {
        foreach (var location in locations)
        {
            if (_locationCells.Exists(locaitonCell => locaitonCell.Data == location))
                continue;

            TryCreateSupppercell(location);

            Cell<ILocation> newLocationCell = CreateCellView(location, location.Data.Superlocation != null ?
                _supperlocationCells[location.Data.Superlocation].View.SubcellsContainer : _cellsContainer);

            newLocationCell.SetInteractable(location.IsAvailable);

            _locationCells.Add(newLocationCell);
        }

        foreach (var locationCell in _locationCells)
            locationCell.Clicked += OnLocationSelected;
    }

    public void Remove(IEnumerable<ILocation> locations)
    {
        foreach (var location in locations)
        {
            location.Dispose();
            Cell<ILocation> locationCell = _locationCells.Find(locationCell => locationCell.Data == location);

            if (locationCell != null)
            {
                locationCell.Clicked -= OnLocationSelected;

                _locationCells.Remove(locationCell);
                locationCell.Dispose();
            }
        }
    }

    private bool TryCreateSupppercell(ILocation location)
    {
        if (location.Data.Superlocation != null)
        {
            if (_supperlocationCells.ContainsKey(location.Data.Superlocation) == false)
            {
                SupperCell<Superlocation> supperCell = _supperCellsFactory.CreateSupperCellView(location.Data.Superlocation, _cellsContainer);

                _supperlocationCells.Add(location.Data.Superlocation, supperCell);
                supperCell.Clicked += OnSuppercellCliced;
            }

            return true;
        }

        return false;
    }

    private Cell<ILocation> CreateCellView(ILocation location, Transform cellContainer)
    {
        return _cellsFactory.CreateCellsView(new List<ILocation>() { location }, cellContainer)[0];
    }

    private void OnSuppercellCliced(CellView suppercellView)
    {
        if (_currentOpenedSuppercell == suppercellView)
        {
            _currentOpenedSuppercell.SwitchSubcellsEnable();
            return;
        }

        _currentOpenedSuppercell = suppercellView;

        HideAllSubcells();
        suppercellView.ChangeSubcellsEnable(true);
    }

    private void OnLocationSelected(ILocation location)
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