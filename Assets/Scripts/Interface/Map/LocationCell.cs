using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LocationCell : MonoBehaviour
{
    public event Action<Location, Action> OnLocationSelected;

    [SerializeField] private TMP_Text _locationNameText;

    private Button _selfButton;
    private Location _selfLocation;

    public LocationType LocationType => _selfLocation.LocationType;

    private void Awake()
    {
        _selfButton = GetComponent<Button>();   
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(() => OnLocationSelected?
            .Invoke(_selfLocation, () => _selfLocation.Show()));
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
    }

    public void Initialize(Location location)
    {
        _selfLocation = location;
        _locationNameText.text = location.Name;
    }
}