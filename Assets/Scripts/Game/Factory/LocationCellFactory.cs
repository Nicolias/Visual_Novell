using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Factory.CellLocation
{
    public class LocationCellFactory : MonoBehaviour, ILocationCellsFactory
    {
        [Inject] private DiContainer _di;
        [SerializeField] private LocationCell _locationCellTemplate;

        public List<LocationCell> CreateNewLocationCell(IEnumerable<Location> locations, Transform locationCellContainer)
        {
            List<LocationCell> locationCells = new List<LocationCell>();

            foreach (var location in locations)
            {
                var newLocationCell = _di.InstantiatePrefabForComponent<LocationCell>(_locationCellTemplate, locationCellContainer);
                newLocationCell.Initialize(location);
                locationCells.Add(newLocationCell);
            }

            return locationCells;
        }
    }
}