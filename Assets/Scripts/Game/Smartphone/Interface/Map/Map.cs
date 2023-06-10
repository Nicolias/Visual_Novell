using Factory.CellLocation;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Map : MonoBehaviour
{
    [Inject] private LocationCellFactory _locationCellFactory;
    [Inject] private ChoicePanel _choicePanel;

    [Inject] private BackgroundView _background;
    [Inject] private CollectionPanel _collectionPanel;

    [Inject] private Smartphone _smartphone;

    [SerializeField] private List<Location> _locations;
    [SerializeField] private Transform _locationCellContainer;

    [SerializeField] private Button _closeButton, _openButton;

    [SerializeField] private Canvas _selfCanvas;

    private List<LocationCell> _locationCells = new();
    private Location _currentLocation;

    private void Awake()
    {
        for (int i = 0; i < _locations.Count; i++)
        {
            _locations[i].Initialize(_background, _collectionPanel);
        }
    }

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(Hide);
        _openButton.onClick.AddListener(Show);

        _locationCells = _locationCellFactory.CreateNewLocationCell(_locations, _locationCellContainer);

        foreach (var locationCell in _locationCells)
            locationCell.OnLocationSelected += OnLocationSelect;
    }

    private void OnDisable()
    {
        foreach (Transform locationCell in _locationCellContainer)
            Destroy(locationCell.gameObject);

        foreach (var locationCell in _locationCells)
            locationCell.OnLocationSelected -= OnLocationSelect;

        _openButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        _selfCanvas.enabled = true;
    }

    private void Hide()
    {
        _selfCanvas.enabled = false;
    }

    public void ChangeLocation(LocationType locationType)
    {
        if (_currentLocation != null)
            _currentLocation.Dispose();

        _currentLocation = _locations.Find(x => x.LocationType == locationType);
        _currentLocation.Show();
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
            })
        });
    }
}
