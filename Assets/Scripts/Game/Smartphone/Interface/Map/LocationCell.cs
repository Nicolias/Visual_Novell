using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LocationCell : MonoBehaviour
{
    [SerializeField] private TMP_Text _locationNameText;

    private Button _selfButton;
    private Location _selfLocation;

    public Location Location => _selfLocation;

    public virtual event Action<Location> LocationSelected;

    private void Awake()
    {
        _selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(
            () => LocationSelected?.Invoke(_selfLocation));
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveAllListeners();
    }

    public void Initialize(Location location)
    {
        _selfLocation = location;

        if(_locationNameText != null)
            _locationNameText.text = location.Name;
    }
}