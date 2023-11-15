using System.Collections.Generic;
using UnityEngine;

public interface ICellsFactory<T>
{
    public List<Cell<T>> CreateCellsView(IEnumerable<T> dataForCells, Transform cellsContainer);
}
