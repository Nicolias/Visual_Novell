using UnityEngine;

public interface ISuppercellsFactory<T>
{
    public SupperCell<T> CreateSupperCellView(T dataForCells, Transform cellsContainer);
}