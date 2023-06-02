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

    [SerializeField] private List<Location> _locations;
    [SerializeField] private Transform _locationCellContainer;

    [SerializeField] private Button _closeButton;

    private List<LocationCell> _locationCells;

    private void OnEnable()
    {
        _locationCells = _locationCellFactory.CreateNewLocationCell(_locations, _locationCellContainer);

        foreach (var locationCell in _locationCells)
            locationCell.OnLocationSelected += OnLocationSelect;

        _closeButton.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        foreach (Transform locationCell in _locationCellContainer)
            Destroy(locationCell.gameObject);

        foreach (var locationCell in _locationCells)
            locationCell.OnLocationSelected -= OnLocationSelect;

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
            })
        });
    }
}
