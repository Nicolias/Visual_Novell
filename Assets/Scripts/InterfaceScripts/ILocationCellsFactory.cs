using System.Collections.Generic;
using UnityEngine;

public interface ILocationCellsFactory
{
    public List<LocationCell> CreateNewLocationCell(IEnumerable<Location> locations, Transform locationCellContainer);
}