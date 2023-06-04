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

    private List<LocationCell> _locationCells = new();

    private void Awake()
    {
        for (int i = 0; i < _locations.Count; i++)
        {
            _locations[i].Initialize(_background, _collectionPanel);
        }
        _closeButton.onClick.AddListener(Hide);
        _openButton.onClick.AddListener(Show);

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
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

    }

    private void OnDestroy()
    {
        _openButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ChangeLocation(LocationType locationType)
    {
        var location = _locations.Find(x => x.LocationType == locationType);
        location.Show();
    }

    private void OnLocationSelect(Location location, Action action)
    {
        _choicePanel.Show($"Перейти на локацию {location.Name}", new()
        {
            new("Подвердить", () => 
            {
                action?.Invoke();
                Hide();
                _choicePanel.Hide();
                _smartphone.Hide();
            })
        });
    }
}
