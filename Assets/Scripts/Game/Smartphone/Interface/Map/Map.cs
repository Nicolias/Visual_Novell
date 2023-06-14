using Factory.CellLocation;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using Zenject;

public class Map : MonoBehaviour, ISaveLoadObject
{
    [Inject] private SaveLoadServise _saveLoadServise;

    [Inject] private LocationCellFactory _locationCellFactory;

    [Inject] private BackgroundView _background;
    [Inject] private CollectionPanel _collectionPanel;

    [Inject] private Smartphone _smartphone;

    [SerializeField] private GameCommander _gameCommander;
    [SerializeField] private ChoicePanel _choicePanel;

    [SerializeField] private List<Location> _locations;
    [SerializeField] private Transform _locationCellContainer;

    [SerializeField] private Button _closeButton, _openButton;

    [SerializeField] private Canvas _selfCanvas;

    private List<LocationCell> _locationCells = new();
    private Location _currentLocation;

    private const string _saveKey = "MapSave";

    private void Awake()
    {
        for (int i = 0; i < _locations.Count; i++)
            _locations[i].Initialize(_background, _collectionPanel);
    }

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(Hide);
        _openButton.onClick.AddListener(Show);

        _locationCells = _locationCellFactory.CreateNewLocationCell(_locations, _locationCellContainer);

        foreach (var locationCell in _locationCells)
            locationCell.OnLocationSelected += OnLocationSelect;

        if (_saveLoadServise.HasSave(_saveKey))
        {
            Load();
            if (_currentLocation != null)
                ChangeLocation(_currentLocation.LocationType);
        }
    }

    private void OnDisable()
    {
        Save();

        foreach (Transform locationCell in _locationCellContainer)
            Destroy(locationCell.gameObject);

        foreach (var locationCell in _locationCells)
            locationCell.OnLocationSelected -= OnLocationSelect;

        _openButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
    }

    public void Show()
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

    public void SetQuest(LocationType locationType, Node quest)
    {
        _locations.Find(x => x.LocationType == locationType).SetQuest(quest);
    }

    public void ExitFromAllLocations()
    {
        _currentLocation = null;
        enabled = false;
    }

    public void SetEnabled(bool eneble)
    {
        _saveLoadServise.Save(_saveKey, new SaveData.MapData() { Enabled = eneble });
        _openButton.enabled = eneble;
    }

    public void ChangeLocation(LocationType locationType)
    {
        if (_currentLocation != null)
            _currentLocation.Dispose();

        _currentLocation = _locations.Find(x => x.LocationType == locationType);
        _currentLocation.Show();

        if (_currentLocation.QuestOnLocation != null)
        {
            _gameCommander.PackAndExecuteCommand(_currentLocation.QuestOnLocation);
            _currentLocation.StartQuest();
        }
    }

    private void OnLocationSelect(Location location, Action action)
    {
        _choicePanel.Show($"Перейти на локацию {location.Name}", new()
        {
            new("Подвердить", () => 
            {
                if (_currentLocation != null)
                    _currentLocation.Dispose();

                action?.Invoke();
                Hide();
                _choicePanel.Hide();
                _smartphone.Hide();
                _currentLocation = location;

                if (_currentLocation.QuestOnLocation != null)
                {
                    _gameCommander.PackAndExecuteCommand(_currentLocation.QuestOnLocation);
                    _currentLocation.StartQuest();
                }
            })
        });
    }

    public void Save()
    {
        for (int i = 0; i < _locations.Count; i++)
            _saveLoadServise.Save($"{_saveKey}/{i}", new SaveData.NodeData() { Node = _locations[i].QuestOnLocation });

        if (_currentLocation != null)
            _saveLoadServise.Save(_saveKey, new SaveData.MapData()
            {
                Enabled = _openButton.enabled,
                CurrentLocationIndex = _locations.IndexOf(_currentLocation)
            });
        else
            _saveLoadServise.Save(_saveKey, new SaveData.MapData()
            {
                Enabled = _openButton.enabled,
                CurrentLocationIndex = -1
            });
    }

    public void Load()
    {
        var data = _saveLoadServise.Load<SaveData.MapData>(_saveKey);

        _openButton.enabled = data.Enabled;

        if (data.CurrentLocationIndex != -1)
            _currentLocation = _locations[data.CurrentLocationIndex];

        for (int i = 0; i < _locations.Count; i++)
            _locations[i].SetQuest(_saveLoadServise.Load<SaveData.NodeData>($"{_saveKey}/{i}").Node);
    }
}
