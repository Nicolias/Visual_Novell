using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Factory.CellLocation
{
    public class LocationCellFactory : MonoBehaviour
    {
        [Inject] private DiContainer _di;
        [SerializeField] private LocationCell _locationCellTemplate;

        public List<LocationCell> CreateNewLocationCell(List<Location> locations, Transform locationCellContainer)
        {
            List<LocationCell> locationCells = new();

            foreach (var location in locations)
            {
                var newlocationCell = _di.InstantiatePrefabForComponent<LocationCell>(_locationCellTemplate, locationCellContainer);
                newlocationCell.Initialize(location);
                locationCells.Add(newlocationCell);
            }

            return locationCells;
        }
    }
}